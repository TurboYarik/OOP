using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Aspose.Cells;
using System.Data.SQLite;
using System.Diagnostics;
using System.Data.Common;
using System.Data;
//using CHESSER;

namespace chessVisualized
{
    class SQLpro
    {
        public void sqlbase()
        {
            string baseName = "CompanyWorkers.db3";

            SQLiteConnection.CreateFile(baseName);

            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = "Data Source = " + baseName;
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"CREATE TABLE [workers] (
                    [id] integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                    [name] char(100) NOT NULL,
                    [family] char(100) NOT NULL,
                    [age] int NOT NULL,
                    [profession] char(100) NOT NULL
                    );";
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
    }

    class scoredmove
    {
        public int score;
        public piece bestpiece;
        public move bestmove;

        public scoredmove(int ps, piece bp, move bm)
        { score = ps; bestpiece = bp; bestmove = bm; }
     }
    class possibleMove
    {
        public List<move> listofmoves;
        public piece piece;

        public possibleMove(List<move> plist, piece p)
        { listofmoves = plist; piece = p; }
    }
    class move
    {
        public int row, col;

        public move(int prow, int pcol)
        { row = prow; col = pcol; }
    }
    class piece
    {
        public int row, col;
        public piece(int r, int c)
        {
            row = r;
            col = c;
        }
    }
    class chess
    {
        int[,] board;
        int boardheight, boardwidth;
        int sw, sh;
        Form1 f;
        piece piecetomove;
        int player;
        List<int> capturedWhitePieces;
        List<int> capturedBlackPieces;
        piece checkSrc;
        int wKingMoved, wQueenRookMoved,
            wKingRookMoved, bKingMoved,
            bQueenRookMoved, bKingRookMoved;
        int whiteTotalMaterials;
        int blackTotalMaterials;
        public char promotionChoice;
        int moverow, movecol;
        public int mode;

        public void passform(Form1 pf)
        {
            f = pf;
        }
        public chess(Graphics g, int bw, int bh)
        {
            board = new int[8,8];
            boardheight = bh;
            boardwidth = bw;
            sw = bw / bw;
            sh = bh / bh;
            player = 1;
            capturedWhitePieces = new List<int>();
            capturedBlackPieces = new List<int>();
            wKingMoved = 0;
            wQueenRookMoved = 0;
            wKingRookMoved = 0;
            bKingMoved = 0;
            bQueenRookMoved = 0;
            bKingRookMoved = 0;
            calcMaterials();
        }
        public void initializeBoard()
        {
            f.Invoke(f.delCaptured, capturedWhitePieces, capturedBlackPieces);
            board[0,0] = -2;
            board[0, 1] = -3;
            board[0, 2] = -4;
            board[0, 3] = -5;
            board[0, 4] = -6;
            board[0, 5] = -4;
            board[0, 6] = -3;
            board[0, 7] = -2;
            for (int i = 0; i < 8; i++)
                board[1, i] = -1;
            for (int row = 2; row < 6; row++)
                for (int col = 0; col < 8; col++)
                    board[row, col] = 0;
            for (int i = 0; i < 8; i++)
                board[6, i] = 1;
            board[7, 0] = 2;
            board[7, 1] = 3;
            board[7, 2] = 4;
            board[7, 3] = 5;
            board[7, 4] = 6;
            board[7, 5] = 4;
            board[7, 6] = 3;
            board[7, 7] = 2;
            
            player = 1;
            wKingMoved = 0;
            wQueenRookMoved = 0;
            wKingRookMoved = 0;
            bKingMoved = 0;
            bQueenRookMoved = 0;
            bKingRookMoved = 0;
            capturedWhitePieces.Clear();
            capturedBlackPieces.Clear();
            calcMaterials();
            f.Invoke(f.updatestat, "", whiteTotalMaterials, blackTotalMaterials);
        }
        private void calcMaterials()
        {
            whiteTotalMaterials = 0;
            blackTotalMaterials = 0;

            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                {
                    switch (board[row, col])
                    {
                        case -1:
                            blackTotalMaterials += 1;
                            break;
                        case 1:
                            whiteTotalMaterials += 1;
                            break;
                        case -2:
                            blackTotalMaterials += 5;
                            break;
                        case 2:
                            whiteTotalMaterials += 5;
                            break;
                        case -3:
                            blackTotalMaterials += 3;
                            break;
                        case 3:
                            whiteTotalMaterials += 3;
                            break;
                        case -4:
                            blackTotalMaterials += 3;
                            break;
                        case 4:
                            whiteTotalMaterials += 3;
                            break;
                        case -5:
                            blackTotalMaterials += 9;
                            break;
                        case 5:
                            whiteTotalMaterials += 9;
                            break;
                    }
                }
        }
        public void ReverseBoard()
        {
            int[,] tmp = new int[8, 8];
            for (int row = 7;row >=0 ;row--)
            {
                for(int col = 7; col >= 0; col--)
                {
                    tmp[row,col] = board[row,col];
                }

            }
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col<  0; col++)
                {
                    board[row, col] = tmp[row, col];
                }

            }
            
        }
        public void displayBoard()
        {
            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                    f.Invoke(f.updatebuttons, row, col, board[row,col]);
            displayCaptured();
        }
        private void displayCaptured()
        {
            f.Invoke(f.captured, capturedWhitePieces, capturedBlackPieces);
        }
        public bool selectPiece(int row, int col)
        {
            if (row < 0 || row >= boardheight
                || col < 0 || col >= boardwidth
                || board[row, col] == 0)
                return false;
            else if (!playersPiece(row, col))
                return false;
            piecetomove = new piece(row, col);
            return true;
        }
        private bool playersPiece(int row, int col)
        {
            if (player % 2 == 0 && board[row, col] < 0 && mode == 1)
                return true;
            else if (player % 2 != 0 && board[row, col] > 0)
                return true;
            return false;
        }
        public bool humanMove(int row, int col)
        {
            bool wantstopromote = false;
            if (player % 2 != 0 && wpawnDoubleJump) wpawnDoubleJump = false;
            else if (player % 2 == 0 && bpawnDoubleJump) bpawnDoubleJump = false;
            f.Invoke(f.delCaptured, capturedWhitePieces, capturedBlackPieces);
            bool valid = false;
            moverow = row;
            movecol = col;
            if (row < 0 || row >= boardheight || col < 0 || col >= boardwidth)
                valid = false;
            else if (board[row, col] * board[piecetomove.row, piecetomove.col] > 0)
                valid = false; 
            else
            {
                switch (board[piecetomove.row, piecetomove.col])
                {
                    case -1:
                    case 1:
                        valid = movePawn(row, col, false);
                        if (valid && (row == 0 || row == 7))
                            wantstopromote = true;
                        break;
                    case -2:
                    case 2:
                        valid = moveRook(row, col);
                        break;
                    case -3:
                    case 3:
                        valid = moveKnight(row, col);
                        break;
                    case -4:
                    case 4:
                        valid = moveBishop(row, col);
                        break;
                    case -5:
                    case 5:
                        valid = moveQueen(row, col);
                        break;
                    case -6:
                    case 6:
                        if (Math.Abs(piecetomove.col - col) == 2)
                            valid = canCastle(row, col);
                        else valid = moveKing(row, col);
                        break;
                }
                if (valid)
                {
                    int target = board[row, col];
                    makemove(row, col); 
                    if (kingCheck(false))
                        valid = false; 
                    if (!valid) undomove(row, col, target);
                    else
                    {
                       
                        if (target < 0) capturedBlackPieces.Add(target);
                        else if (target > 0) capturedWhitePieces.Add(target);
                        
                        if (piecetomove.row == 0 && piecetomove.col == 4)
                            bKingMoved = 1;
                        else if (piecetomove.row == 7 && piecetomove.col == 4)
                            wKingMoved = 1;
                        else if (piecetomove.row == 0 && piecetomove.col == 0)
                            bQueenRookMoved = 1;
                        else if (piecetomove.row == 0 && piecetomove.col == 7)
                            bKingRookMoved = 1;
                        else if (piecetomove.row == 7 && piecetomove.col == 0)
                            wQueenRookMoved = 1;
                        else if (piecetomove.row == 7 && piecetomove.col == 7)
                            wKingRookMoved = 1;
                        if (wantstopromote)
                            providePromotionOptions();
                        player++;
                        ReverseBoard();
                        f.Invoke(f.updatestat, "", whiteTotalMaterials, blackTotalMaterials);
                        calcMaterials();
                        if (isCheckmate() == 0 || isCheckmate() == 1 && player % 2 == 1)
                        {
                            f.Invoke(f.updatestat, "153504 ОТЧИСЛЕНА!!!", whiteTotalMaterials, blackTotalMaterials);
                           
                        }     
                        else
                        {
                            if (isCheckmate() == 0 || isCheckmate() == 1 && player % 2 == 0)
                                f.Invoke(f.updatestat, "153504 СДАЛА СЕССИЮ!!!!", whiteTotalMaterials, blackTotalMaterials);

                            if (isStalemate())
                                f.Invoke(f.updatestat, "КТО-ТО НА ПЕРЕСДАЧУ", whiteTotalMaterials, blackTotalMaterials);
                        }
                        if(isCheckmate()!=1 && isCheckmate()!=0)
                        {
                            if (kingCheck(false))
                                f.Invoke(f.updatestat, "Произошло нападение", whiteTotalMaterials, blackTotalMaterials);
                            else
                                f.Invoke(f.updatestat, "Продолжаем играть", whiteTotalMaterials, blackTotalMaterials);
                            if (mode == 0 && !wantstopromote)
                            {
                                displayBoard();
                                f.Invoke(f.delCaptured, capturedWhitePieces, capturedBlackPieces);
                            }
                        }
                    }
                }
            }
            return valid;
        }
        public void makemove(int row, int col)
        {
            board[row, col] = board[piecetomove.row,piecetomove.col];
            board[piecetomove.row, piecetomove.col] = 0;
        }
        private void undomove(int row, int col, int target)
        {
            board[piecetomove.row, piecetomove.col] = board[row, col];
            board[row, col] = target;
        }
        bool wpawnDoubleJump;
        bool bpawnDoubleJump;
        private bool movePawn(int row, int col, bool testingCheck)
        {
            bool valid = false;
            if ((piecetomove.row - row) * board[piecetomove.row, piecetomove.col] > 0)
            { // moving right direction
                if (Math.Abs(piecetomove.row - row) == 1 && piecetomove.col == col && 
                    board[row, col] == 0)
                    valid = true;
                else if (Math.Abs(piecetomove.row - row) == 2 && 
                    board[(piecetomove.row + row) / 2, col] == 0 &&
                    board[row, col] == 0 && piecetomove.col == col &&
                    ((piecetomove.row == 6 && player % 2 != 0) ||
                    (piecetomove.row == 1 && player % 2 == 0)))
                {
                    if (!testingCheck)
                    {
                        if (player % 2 != 0) wpawnDoubleJump = true;
                        else bpawnDoubleJump = true;
                    }
                    valid = true;
                }
                else if (Math.Abs(piecetomove.row - row) == 1 && 
                    Math.Abs(piecetomove.col - col) == 1)
                {
                    if ((board[row, col] * board[piecetomove.row, piecetomove.col] < 0))
                        valid = true;
                    else
                    {
                        if (board[piecetomove.row, col] *
                            board[piecetomove.row, piecetomove.col] < 0 &&
                            ((player % 2 != 0 && bpawnDoubleJump == true) ||
                            (player % 2 == 0 && wpawnDoubleJump == true)) && !testingCheck)
                        { 
                            if (player % 2 != 0)
                                capturedBlackPieces.Add(-1);
                            else
                                capturedWhitePieces.Add(1);
                            board[piecetomove.row, col] = 0;
                            valid = true;
                        }
                    }
                }
            }
            return valid;
        }
        private bool moveRook(int row, int col)
        {
            bool valid = true;
            
            if (piecetomove.row == row)
            { 
                int offset = col - piecetomove.col;
                int range = Math.Abs(offset);

                for (int i = 1; i < range; i++)
                    if (board[piecetomove.row, piecetomove.col + i * offset / range] != 0)
                    {
                        valid = false;
                        break;
                    }
            }
            else if (piecetomove.col == col)
            { 
                int offset = row - piecetomove.row;
                int range = Math.Abs(offset);

                for (int i = 1; i < range; i++)
                    if (board[piecetomove.row + i * offset / range, piecetomove.col] != 0)
                    {
                        valid = false;
                        break;
                    }
            }
            else valid = false;

            return valid;
        }
        private bool moveKnight(int row, int col)
        {
            bool valid = false;

            if (piecetomove.row != row && piecetomove.col != col &&
                (Math.Abs(row - piecetomove.row) + Math.Abs(col - piecetomove.col) == 3))
                valid = true;

            return valid;
        }
        private bool moveBishop(int row, int col)
        {
            bool valid = true;

            if (Math.Abs(piecetomove.row - row) == Math.Abs(piecetomove.col - col))
            {
                int offsetcol = col - piecetomove.col;
                int offsetrow = row - piecetomove.row;
                int range = Math.Abs(offsetcol);

                for (int i = 1; i < range; i++)
                    if (board[piecetomove.row + i * offsetrow / range,
                        piecetomove.col + i * offsetcol / range] != 0)
                    {
                        valid = false;
                        break;
                    }
            }
            else valid = false;

            return valid;
        }
        private bool moveQueen(int row, int col)
        {
            bool valid = false;
            
            if (piecetomove.row == row || piecetomove.col == col)
                valid = moveRook(row, col);
            else if (Math.Abs(piecetomove.row - row) == Math.Abs(piecetomove.col - col))
                valid = moveBishop(row, col);

            return valid;
        }
        private bool moveKing(int row, int col)
        {
            bool valid = false;
            
            if ((Math.Abs(piecetomove.row - row) == 1 && piecetomove.col == col)
                || (Math.Abs(piecetomove.col - col) == 1 && piecetomove.row == row)
                || (piecetomove.row != row && piecetomove.col != col
                && Math.Abs(piecetomove.row - row) + Math.Abs(piecetomove.col - col) == 2))
                valid = true;

            return valid;
        }
        private bool kingCheck(bool testingCheckMate)
        {
            piece king = new piece(-1, -1);
            piece tmp = new piece(piecetomove.row, piecetomove.col);

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (((board[row, col] == 6) && player % 2 != 0) ||
                        ((board[row, col] == -6) && player % 2 == 0))
                    {
                        king.row = row;
                        king.col = col;
                        break;
                    }
                }
                if (king.row != -1) break;
            }
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    piecetomove.row = r;
                    piecetomove.col = c;
                    if (board[king.row, king.col] * board[r, c] < 0)
                    {
                        switch (board[r, c])
                        {
                            case -1:
                            case 1:
                                if (movePawn(king.row, king.col, true))
                                {
                                    if (testingCheckMate)
                                        checkSrc = piecetomove;
                                    piecetomove = tmp; 
                                    return true;
                                }
                                break;
                            case -2:
                            case 2:
                                if (moveRook(king.row, king.col))
                                {
                                    if (testingCheckMate)
                                        checkSrc = piecetomove;
                                    piecetomove = tmp; 
                                    return true;
                                }
                                break;
                            case -3:
                            case 3:
                                if (moveKnight(king.row, king.col))
                                {
                                    if (testingCheckMate)
                                        checkSrc = piecetomove;
                                    piecetomove = tmp;
                                    return true;
                                }
                                break;
                            case -4:
                            case 4:
                                if (moveBishop(king.row, king.col))
                                {
                                    if (testingCheckMate)
                                        checkSrc = piecetomove;
                                    piecetomove = tmp;
                                    return true;
                                }
                                break;
                            case -5:
                            case 5:
                                if (moveQueen(king.row, king.col))
                                {
                                    if (testingCheckMate)
                                        checkSrc = piecetomove;
                                    piecetomove = tmp; 
                                    return true;
                                }
                                break;
                        }
                    }
                }
            }
            piecetomove = tmp; 
            return false;
        }
        private bool canCastle(int row, int col)
        {
            bool cc = false;
            if (!kingCheck(false))
            {
                if (player % 2 == 0)
                {
                    if (bKingMoved == 0)
                    {
                        if (col == 2 && bQueenRookMoved == 0 && board[row, 0] == -2)
                            cc = true;
                        else if (col == 6 && bKingRookMoved == 0 && board[row, 7] == -2)
                            cc = true;
                    }
                }
                else
                {
                    if (wKingMoved == 0)
                    {
                        if (col == 2 && wQueenRookMoved == 0 && board[row, 0] == 2)
                            cc = true;
                        else if (col == 6 && wKingRookMoved == 0 && board[row, 0] == 2)
                            cc = true;
                    }
                }
                if (cc == true)
                {
                    int offset;
                    int range;
                    if (col == 2)
                    {
                        offset = -4;
                        range = 4;
                    }
                    else
                    {
                        offset = 3;
                        range = 3;
                    }
                    for (int i = 1; i < range; i++) 
                        if (board[piecetomove.row, piecetomove.col + i * offset / range] != 0)
                            cc = false;
                    if (cc == true) 
                        for (int i = 1; i < 3; i++)
                        { 
                            piece trialDest = new piece(row, piecetomove.col + i * offset / range);
                            makemove(row, trialDest.col);
                            if (kingCheck(false)) cc = false;
                            undomove(row, trialDest.col, 0);
                            if (cc == false) break;
                        }
                }
                if (cc == true)
                {
                    if (row == 0 && col == 2)
                    {
                        board[0, 0] = 0;
                        board[0, 3] = -2;
                    }
                    else if (row == 0 && col == 6)
                    {
                        board[0, 7] = 0;
                        board[0, 5] = -2;
                    }
                    else if (row == 7 && col == 2)
                    {
                        board[7, 0] = 0;
                        board[7, 3] = 2;
                    }
                    else if (row == 7 && col == 6)
                    {
                        board[7, 7] = 0;
                        board[7, 5] = 2;
                    }
                }
            }
            return cc;
        }
        public bool isStalemate()
        {
            if (whiteTotalMaterials == 0 && blackTotalMaterials == 0)
                return true;
            else if (whiteTotalMaterials == 0 || blackTotalMaterials == 0)
                if (insufficientMaterial())
                    return true; 
            else if (!kingCheck(false) && ((whiteTotalMaterials == 0 && player % 2 != 0)
                || (blackTotalMaterials == 0 && player % 2 == 0)))
                if (!kingCanMove())
                    return true; 
            return false;
        }
        private bool insufficientMaterial()
        {
            if (whiteTotalMaterials == 3 || blackTotalMaterials == 3)
            {
                for (int row = 0; row < 8; row++)
                    for (int col = 0; col < 8; col++)
                    {
                        if ((board[row, col] == 1 && whiteTotalMaterials == 3)
                            || (board[row, col] == -1 && blackTotalMaterials == 3))
                            return false;
                    }
                return true;
            }
            return false;
        }
        piece king;
        private bool kingCanMove()
        {
            piece dest;
            piece tmp = new piece(piecetomove.row, piecetomove.col); 
            int[,] offset = new int[,] {
                {1,0},
                {0,1},
                {-1,0},
                {0,-1},
                {1,1},
                {1,-1},
                {-1,1},
                {-1,-1}
            };
            bool kingFound = false;
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if ((board[row, col] == 6 && player % 2 != 0) ||
                        (board[row, col] == -6 && player % 2 == 0))
                    {
                        piecetomove.row = row;
                        piecetomove.col = col;
                        king = new piece(row, col);
                        kingFound = true;
                        break;
                    }
                }
                if (kingFound) break;
            }
            for (int i = 0; i < 8; i++)
            {
                dest = new piece(piecetomove.row + offset[i, 0], piecetomove.col + offset[i, 1]);
                if (dest.row > -1 && dest.row < 8 && dest.col > -1 && dest.col < 8)
                {
                    if ((board[piecetomove.row, piecetomove.col] * board[dest.row, dest.col] <= 0)
                        && moveKing(dest.row, dest.col))
                    { 
                        int target = board[dest.row, dest.col];
                        makemove(dest.row, dest.col);
                        if (!kingCheck(false))
                        {
                            undomove(dest.row, dest.col, target);
                            piecetomove = tmp; 
                            return true;
                        }
                        undomove(dest.row, dest.col, target);
                    }
                }
            }
            piecetomove = tmp; 
            return false;
        }
        public int isCheckmate()
        {
            if (!kingCheck(true))
                return -1;
            if (kingCanMove())
                return -1;
            
            int offsetInt, range;
            int offsetcol, offsetrow;
            piece[] threatArray;

            if (king.row == checkSrc.row)
            {
                offsetInt = checkSrc.col - king.col;
                range = Math.Abs(offsetInt);

                threatArray = new piece[range];

                for (int i = 0; i < range; i++)
                    threatArray[i] = new piece(king.row, king.col + (i+1) * offsetInt / range);
            }
            else if (king.col == checkSrc.col)
            {
                offsetInt = checkSrc.row - king.row;
                range = Math.Abs(offsetInt);

                threatArray = new piece[range];

                for (int i = 0; i < range; i++)
                    threatArray[i] = new piece(king.row + (i+1) * offsetInt / range, king.col);
            }
            else
            {
                offsetcol = checkSrc.col - king.col;
                offsetrow = checkSrc.row - king.row;
                range = Math.Abs(offsetcol);

                threatArray = new piece[range];

                for (int i = 0; i < range; i++)
                    threatArray[i] = new piece(king.row + (i+1) * offsetrow / range,
                        king.col + (i+1) * offsetcol / range);
            }
            piece tmp = new piece(piecetomove.row, piecetomove.col); 
            for (int r = 0; r < 8; r++) 
                for (int c = 0; c < 8; c++)
                {
                    if (board[r, c] * board[king.row, king.col] > 0)
                    {
                        piecetomove.row = r;
                        piecetomove.col = c;
                        for (int i = 0; i < range; i++)
                        {
                            switch (board[r, c])
                            {
                                case -1:
                                case 1:
                                    if (movePawn(threatArray[i].row, threatArray[i].col, true))
                                    {
                                        piecetomove = tmp; 
                                        return -1;
                                    }
                                    break;
                                case -2:
                                case 2:
                                    if (moveRook(threatArray[i].row, threatArray[i].col))
                                    {
                                        piecetomove = tmp;
                                        return -1;
                                    }
                                    break;
                                case -3:
                                case 3:
                                    if (moveKnight(threatArray[i].row, threatArray[i].col))
                                    {
                                        piecetomove = tmp; 
                                        return -1;
                                    }
                                    break;
                                case -4:
                                case 4:
                                    if (moveBishop(threatArray[i].row, threatArray[i].col))
                                    {
                                        piecetomove = tmp; 
                                        return -1;
                                    }
                                    break;
                                case -5:
                                case 5:
                                    if (moveQueen(threatArray[i].row, threatArray[i].col))
                                    {
                                        piecetomove = tmp; 
                                        return -1;
                                    }
                                    break;
                            }
                        }
                    }
                }
            piecetomove = tmp; 
            return 1;
        }
        private void providePromotionOptions()
        {
            f.Invoke(f.promote);
        } 
        public void promotePawn()
        {
            player--;
            int row = moverow;
            int col = movecol;
            switch (promotionChoice)
            {
                case 'Q':
                    if (player % 2 == 0) board[row, col] = -5;
                    else board[row, col] = 5;
                    break;
                case 'B':
                    if (player % 2 == 0) board[row, col] = -4;
                    else board[row, col] = 4;
                    break;
                case 'K':
                    if (player % 2 == 0) board[row, col] = -3;
                    else board[row, col] = 3;
                    break;
                case 'R':
                    if (player % 2 == 0) board[row, col] = -2;
                    else board[row, col] = 2;
                    break;
            }
            f.Invoke(f.promoteDelete); 

            
            if (player % 2 == 0)
            {
                if (capturedBlackPieces.Contains(board[row, col]))
                    capturedBlackPieces.Remove(board[row, col]);
                capturedBlackPieces.Add(-1);
            }
            else
            {
                if (capturedWhitePieces.Contains(board[row, col]))
                    capturedWhitePieces.Remove(board[row, col]);
                capturedWhitePieces.Add(1);
            }
            player++;
            ReverseBoard();
            f.Invoke(f.updatestat, "", whiteTotalMaterials, blackTotalMaterials);
            calcMaterials();
            if (isCheckmate() == 0 || isCheckmate() == 1 && player % 2 == 1)
            {
                f.Invoke(f.updatestat, "153504 ОТЧИСЛЕНА!!!!", whiteTotalMaterials, blackTotalMaterials);
                
            }    
            else
            {
                if (isCheckmate() == 0 || isCheckmate() == 1 && player % 2 == 0)
                    f.Invoke(f.updatestat, "153504 СДАЛА СЕССИЮ!!!!", whiteTotalMaterials, blackTotalMaterials);

                if (isStalemate())
                    f.Invoke(f.updatestat, "КТО-ТО НА ПЕРЕСДАЧУ", whiteTotalMaterials, blackTotalMaterials);
            }
            if (isCheckmate() != 1 && isCheckmate() != 0)
            {
                if (kingCheck(false))
                    f.Invoke(f.updatestat, "Произошло нападение", whiteTotalMaterials, blackTotalMaterials);
                else
                    f.Invoke(f.updatestat, "Продолжаем играть", whiteTotalMaterials, blackTotalMaterials);
                if (mode == 0)
                {
                    displayBoard();
                    f.Invoke(f.delCaptured, capturedWhitePieces, capturedBlackPieces);
                }
            }
            return;
        }
        public void loadGame()
        {
            capturedWhitePieces.Clear();
            capturedBlackPieces.Clear();
            string fileContent = File.ReadAllText(saveFile);
            string[] integerStrings = fileContent.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int[] integers = new int[integerStrings.Length];
            for (int n = 0; n < integerStrings.Length; n++)
                integers[n] = int.Parse(integerStrings[n]);
            player = integers[0];
            int i = 1;
            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++, i++)
                    board[row, col] = integers[i];
           
            for (; integers[i] != -9; i++)
                capturedWhitePieces.Add(integers[i]);
            for (i++; integers[i] != -9; i++)
                capturedBlackPieces.Add(integers[i]);
           
            i++;
            wKingMoved = integers[i++];
            bKingMoved = integers[i++];
            wKingRookMoved = integers[i++];
            wQueenRookMoved = integers[i++];
            bKingRookMoved = integers[i++];
            bQueenRookMoved = integers[i++];
            calcMaterials();
            piecetomove = new piece(-1, -1);
            if (isCheckmate() == 0 || isCheckmate() == 1 && player % 2 == 1)
                f.Invoke(f.updatestat, "153504 ОТЧИСЛЕНА!!!!", whiteTotalMaterials, blackTotalMaterials);
            else
            {
                if (isCheckmate() == 0 || isCheckmate() == 1 && player % 2 == 0)
                    f.Invoke(f.updatestat, "153504 СДАЛА СЕССИЮ!!!!", whiteTotalMaterials, blackTotalMaterials);

                if (isStalemate())
                    f.Invoke(f.updatestat, "КТО-ТО НА ПЕРЕСДАЧУ", whiteTotalMaterials, blackTotalMaterials);
            }
            if (isCheckmate() != 1 && isCheckmate() != 0)
            {
                if (kingCheck(false))
                    f.Invoke(f.updatestat, "Произошло нападение", whiteTotalMaterials, blackTotalMaterials);
                else
                    f.Invoke(f.updatestat, "Продолжаем играть", whiteTotalMaterials, blackTotalMaterials);
            }
        }
        public string saveFile;
        public void saveGame()
        {
            string lines = "";
            lines += (player % 2).ToString() + "\r\n";
           
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                    lines += board[row, col].ToString() + "\t";
                lines += "\r\n";
            }
            for (int i = 0; i < capturedWhitePieces.Count; i++)
                lines += capturedWhitePieces[i] + "\t";
            lines += "-9\r\n";
            for (int i = 0; i < capturedBlackPieces.Count; i++)
                lines += capturedBlackPieces[i] + "\t";
            lines += "-9\r\n";
            lines += wKingMoved + "\t" + bKingMoved + "\r\n";
            lines += wKingRookMoved + "\t" + wQueenRookMoved + "\r\n";
            lines += bKingRookMoved + "\t" + bQueenRookMoved + "\r\n";
            string saveFile1 = "SaveGame1.json";
            string jsonString = JsonSerializer.Serialize(board.ToString());
            System.IO.StreamWriter file = new System.IO.StreamWriter(saveFile);
            
            //var workbook = new Workbook("input.txt");
            //workbook.Save("Output.json");
            file.WriteLine(lines);

            file.Close();
        }
    }
}
