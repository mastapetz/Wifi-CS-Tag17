namespace AdressWeb.Models
{
    public partial class Postleitzahlen
    {
        public string PlzCombo
        {
            get
            {
                return Plz.ToString() + " " + Ort;
            }
        }
    }
}
