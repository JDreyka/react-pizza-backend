using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using react.pizza.backend.Configs;
using react.pizza.backend.Data;
using react.pizza.backend.Models;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace react.pizza.backend.Controllers
{
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private const string messageTemplate =
            "*Оформленный новый заказ:*\n" +
            "\\-\\-\\-\\-\\-\\-\\-\\-\n" +
            "\\[Сумма заказа\\]: {0}\n" +
            "\\[Список товаров\\]:\n" +
            "{1}\n" +
            "\\-\\-\\-\\-\\-\\-\\-\\-\n";

        private const string itemTemplate =
            "   \\> `{0} - x{1}`";

        private readonly ApplicationDbContext applicationDbContext;
        private readonly ITelegramBotClient telegramBotClient;

        private readonly TelegramBotConfig telegramBotConfig;

        public OrderController(
            ApplicationDbContext applicationDbContext,
            ITelegramBotClient telegramBotClient,
            IOptions<TelegramBotConfig> telegramBotConfig)
        {
            this.applicationDbContext = applicationDbContext;
            this.telegramBotClient = telegramBotClient;

            this.telegramBotConfig = telegramBotConfig.Value;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            var itemsIds = order.Items.Select(x => x.Id).ToHashSet();

            var itemsInfo = applicationDbContext.CatalogItems
                .Where(x => itemsIds.Contains(x.Id))
                .ToDictionary(x => x.Id, y => y);

            var orderCost = order.Items
                .Select(x => itemsInfo[x.Id].Cost * x.Count)
                .Sum();

            var itemStrings = string.Join("\n",
                order.Items.Select(x => string.Format(itemTemplate, itemsInfo[x.Id].Title, x.Count)));

            var notifyString = string.Format(messageTemplate, orderCost, itemStrings);

            await telegramBotClient.SendTextMessageAsync(telegramBotConfig.ChatId, notifyString, ParseMode.MarkdownV2);

            return Ok(notifyString);
        }
    }
}