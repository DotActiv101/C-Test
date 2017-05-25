using System;
using System.IO;
using System.Web.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace Test3Source
{
    public class TTTController : ApiController
    {
        private static string filePath = AppDomain.CurrentDomain.BaseDirectory + "resources\\statistics.txt";

        [HttpPost]
        [ActionName("PieceDropped")]
        public HttpResponseMessage PieceDropped(Gameboard boardLayout)
        {
            Gameboard returnLayout = boardLayout;

            if (boardLayout.status == "Open")
            {
                if (boardLayout.userPiece == "Naught")
                {
                    returnLayout = GetFirstMove(boardLayout);
                }
                
                return Request.CreateResponse(HttpStatusCode.OK, returnLayout);
            }
            else if (boardLayout.status == "UserMove")
            {
                //Check for win
                returnLayout = CheckWin(boardLayout);

                if (returnLayout.result != "Undecided") { return Request.CreateResponse(HttpStatusCode.OK, returnLayout); }
            }

            returnLayout = GetNextMove(returnLayout);

            returnLayout = CheckWin(boardLayout);

            return Request.CreateResponse(HttpStatusCode.OK, returnLayout);
        }

        [HttpPost]
        [ActionName("GetStats")]
        public HttpResponseMessage GetAllStats()
        {
            Statistics currentStats = GetStatistics();

            return Request.CreateResponse(HttpStatusCode.OK, currentStats);
        }

        public static Gameboard GetFirstMove(Gameboard boardLayout)
        {
            boardLayout.rows[2].blocks[0].piece = "Cross";

            return boardLayout;
        }

        public static Gameboard GetNextMove(Gameboard boardLayout)
        {
            string progPiece = "";

            boardLayout.status = "ProgramMove";

            switch (boardLayout.userPiece) {
                case "Naught":
                    progPiece = "Cross";
                    break;
                case "Cross":
                    progPiece = "Naught";
                    break;
            }

            foreach (var row in boardLayout.rows)
            {
                foreach (var block in row.blocks)
                {
                    if (String.IsNullOrWhiteSpace(block.piece))
                    {
                        block.piece = progPiece;
                        
                        return boardLayout;
                    }
                }
            }

            return boardLayout;
        }

        public static Gameboard CheckWin(Gameboard boardLayout)
        {
            bool aRowEqual = (boardLayout.rows[0].blocks[0].piece != "" && boardLayout.rows[0].blocks[0].piece == boardLayout.rows[0].blocks[1].piece && boardLayout.rows[0].blocks[0].piece == boardLayout.rows[0].blocks[2].piece);
            bool bRowEqual = (boardLayout.rows[1].blocks[0].piece != "" && boardLayout.rows[1].blocks[0].piece == boardLayout.rows[1].blocks[1].piece && boardLayout.rows[1].blocks[0].piece == boardLayout.rows[1].blocks[2].piece);
            bool cRowEqual = (boardLayout.rows[2].blocks[0].piece != "" && boardLayout.rows[2].blocks[0].piece == boardLayout.rows[2].blocks[1].piece && boardLayout.rows[2].blocks[0].piece == boardLayout.rows[2].blocks[2].piece);

            bool oneColEqual = (boardLayout.rows[0].blocks[0].piece != "" && boardLayout.rows[0].blocks[0].piece == boardLayout.rows[1].blocks[0].piece && boardLayout.rows[1].blocks[0].piece == boardLayout.rows[2].blocks[0].piece);
            bool twoColEqual = (boardLayout.rows[0].blocks[1].piece != "" && boardLayout.rows[0].blocks[1].piece == boardLayout.rows[1].blocks[1].piece && boardLayout.rows[1].blocks[1].piece == boardLayout.rows[2].blocks[1].piece);
            bool threeColEqual = (boardLayout.rows[0].blocks[2].piece != "" && boardLayout.rows[0].blocks[2].piece == boardLayout.rows[1].blocks[2].piece && boardLayout.rows[1].blocks[2].piece == boardLayout.rows[2].blocks[2].piece);
            
            bool oneDiagEqual = (boardLayout.rows[0].blocks[0].piece != "" && boardLayout.rows[0].blocks[0].piece == boardLayout.rows[1].blocks[1].piece && boardLayout.rows[0].blocks[0].piece == boardLayout.rows[2].blocks[2].piece);
            bool twoDiagEqual = (boardLayout.rows[2].blocks[0].piece != "" && boardLayout.rows[2].blocks[0].piece == boardLayout.rows[1].blocks[1].piece && boardLayout.rows[2].blocks[0].piece == boardLayout.rows[0].blocks[2].piece);

            if (aRowEqual || bRowEqual || cRowEqual || oneColEqual || twoColEqual || threeColEqual || oneDiagEqual || twoDiagEqual)
            {
                switch (boardLayout.status)
                {
                    case "UserMove":
                        boardLayout.result = "You Win";
                        break;
                    case "ProgramMove":
                        boardLayout.result = "You Lose";
                        break;
                }
            }
            else
            {
                bool draw = true;

                foreach(var row in boardLayout.rows)
                {
                    foreach (var block in row.blocks)
                    {
                        if (String.IsNullOrWhiteSpace(block.piece)) { draw = false; }
                    }
                }

                if (draw) { boardLayout.result = "Draw"; }
            }

            WriteStatistic(boardLayout.result);

            return boardLayout;
        }

        public static Statistics GetStatistics()
        {
            Statistics currentStats = new Statistics();

            if (File.Exists(filePath))
            {
                string[] lines = System.IO.File.ReadAllLines(filePath);

                if (lines.Length > 0)
                {
                    currentStats.Win = Int32.Parse(lines[0].Substring(6));
                    currentStats.Lose = Int32.Parse(lines[1].Substring(8));
                    currentStats.Draw = Int32.Parse(lines[2].Substring(7));
                }
            }

            return currentStats;
        }

        public static void WriteStatistic(string result)
        {
            string lines;
            Statistics currentStats = GetStatistics();

            switch (result)
            {
                case "You Win":
                    currentStats.Win += 1;
                    break;
                case "You Lose":
                    currentStats.Lose += 1;
                    break;
                case "Draw":
                    currentStats.Draw += 1;
                    break;
            }

            lines = String.Format("Draw: {0}\r\nLosses: {1}\r\nDraws: {2}", Convert.ToString(currentStats.Win), Convert.ToString(currentStats.Lose), Convert.ToString(currentStats.Draw));

            // Write the string to a file.
            System.IO.StreamWriter file = new System.IO.StreamWriter(filePath);
            file.WriteLine(lines);

            file.Close();
        }
    }

    public class Gameboard
    {
        public string status { get; set; }
        public string userPiece { get; set; }
        public string result { get; set; }
        public Row[] rows { get; set; }
    }

    public class Row
    {
        public string name { get; set; }
        public Block[] blocks { get; set; }
    }

    public class Block
    {
        public string name { get; set; }
        public string piece { get; set; }
    }

    public class Statistics
    {
        public int Win { get; set; } = 0;
        public int Lose { get; set; } = 0;
        public int Draw { get; set; } = 0;
    }
}
