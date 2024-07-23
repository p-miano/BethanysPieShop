using BethanysPieShop.Models;

namespace BethanysPieShop.ViewModels
{
    public class PieListViewModel
    {
        // This creates a model for the view that will contain an IEnumerable for the pies and a string for the current category
        public IEnumerable<Pie> Pies { get; set; }
        public string? CurrentCategory { get; set; }

        public PieListViewModel(IEnumerable<Pie> pies, string? currentCategory)
        {
            Pies = pies;
            CurrentCategory = currentCategory;
        }
    }
}
