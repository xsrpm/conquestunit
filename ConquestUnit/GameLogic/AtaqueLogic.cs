using Windows.UI.Xaml.Shapes;

namespace GameLogic
{
    public static class AtaqueLogic
    {
        public static bool TerritorioSePuedeAtacar(Path[,] Territorios, int idTerritorioOrigenAtaque, Path TerritorioAtaqueDestino)
        {
            for (int i = 0; i < 8; i++)
            {
                var frontera = Territorios[idTerritorioOrigenAtaque, i];
                if (frontera != null)
                {
                    if (TerritorioAtaqueDestino.Tag.ToString() == frontera.Tag.ToString())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
