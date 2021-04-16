using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ConditionalFormatting {
    public class ConditionalFormattingViewModel {
        public ConditionalFormattingViewModel() {
            Items = SaleOverviewDataGenerator.GenerateSales();
        }
        public SaleOverviewData[] Items { get; private set; }
    }
}
