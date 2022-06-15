//Script que administra las llamadas a las tablas de clasificacion
//Creado por Alexis Alvarado.
//Fecha: 10/06/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;

public class LBManager : MonoBehaviour
{

    //Recibe el id de la tabla y manda la solicitud de actualización a dicha tabla
    public static void updateScores(int id)
    {
        if (GameObject.FindObjectOfType<GPGSrvcs>().user())
        {
            switch (id)
            {
                case 1:
                    Social.ReportScore(PlayerPrefs.GetInt("BestScore"), LeaderBoardsIds.leaderboard_puntuaciones, null);
                    break;
                case 2:
                    Social.ReportScore(PlayerPrefs.GetInt("BestTime"), LeaderBoardsIds.leaderboard_tiempo_de_juego, null);
                    break;
                case 3:
                    Social.ReportScore(PlayerPrefs.GetInt("Deaths"), LeaderBoardsIds.leaderboard_total_de_muertes, null);
                    break;
                case 4:
                    Social.ReportScore(PlayerPrefs.GetInt("BestFruits"), LeaderBoardsIds.leaderboard_frutas_recolectadas, null);
                    break;
                case 5:
                    Social.ReportScore(PlayerPrefs.GetInt("BestHeight"), LeaderBoardsIds.leaderboard_altura_alcanzada, null);
                    break;
            }
        }
    }

}
