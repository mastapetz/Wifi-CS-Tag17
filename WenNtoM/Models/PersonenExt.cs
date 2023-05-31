using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace WenNtoM.Models
{
    public partial class Personen
    {
        // Properties
        // Liste mit allen Interessen
        // SelectListItem wird für ListBox benötigt
        [NotMapped] // Kenzeichnet das diese Properties nicht in der Datenbank sind,
                    // da EF erwatzez das alle Properties in DB sind
        public IEnumerable<SelectListItem> Interessen { get; set; }

        // Liste der selektierten Items
        // hier werden Primary Key der Interessenstabelle abgelegt
        [NotMapped]
        //public IEnumerable<int> SelectedIntTags { get; set; }
        public IEnumerable<int> SelectedIntTags { get; set; }
    }
}
