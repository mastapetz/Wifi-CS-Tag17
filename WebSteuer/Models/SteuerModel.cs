/* 
 * 
 * Ein Jahreseinkommen unter 11.693 € ist steuerfrei.
 * Das Einkommen von 11.693 bis 19.134 € wird mit 20% versteuert.
 * Von 19.134 bis 32.075 € sind es 30%. 
 * Zwischen 32.075 bis 62.080 € sind es 41% und von 62.080 bis 93.120 € 48% Steuern.
 * Bis 1.000.000 € sind es 50% und darüber 55%. 
 * 
 */
namespace WebSteuer.Models
{
    public class SteuerModel
    {
        // Property für Eingabe
        public double Jahresgehalt { get; set; }

        // Property für Ausgabe
        public double Jahressteuer { get; set; }
        public double Nettojahresgehalt { get; set; }

        // Tarifstufen
        // 0 Dimension Steuergrenzen 
        // 1. Dimension Steuersätze
        private double[,] _stufen =
        {
            {11693, 19134, 32075, 62080, 93120, 1000000 },
            {0.2, 0.30, 0.41, 0.48, 0.5, 0.55},

        };

        // Berechnen
        public void Berechnen()
        {
            double brutto = Jahresgehalt;
            Jahressteuer = 0;

            // Schleife über Steuerstufen
            for (int i = _stufen.GetLength(1) - 1; i >= 0; i--)
            {
                // Einkommen > Tarifstufe?
                if (brutto > _stufen[0, i])
                {
                    // für den Teil der größer ist, wird die Steuer berechnen
                    Jahressteuer += (brutto - _stufen[0, i]) * _stufen[1, i];
                    brutto = _stufen[0, i];
                }
            }

            Nettojahresgehalt = Jahresgehalt - Jahressteuer;
        }
    }
}
