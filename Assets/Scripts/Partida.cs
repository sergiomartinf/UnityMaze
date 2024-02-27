using System;

[Serializable] public class Partida
{
    public string jugador, fecha;
    public int puntos;

    public Partida(string jugador, int puntos, string fecha)
    {
        this.jugador = jugador;
        this.puntos = puntos;
        this.fecha = fecha;
    }
}