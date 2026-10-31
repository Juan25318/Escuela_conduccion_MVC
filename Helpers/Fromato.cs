using System.Globalization;

namespace MVC_Seguimiento3.Helpers;

public static class Formato
{
    public static string Dinero(decimal valor)
    {
        return "$" + valor.ToString("N0", CultureInfo.InvariantCulture).Replace(",", ".");
    }
}