using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace sakura
{

    public delegate void StartNewGame();
    
   public class GameProcess
    {
        public event StartNewGame init;

        public bool isGame;
        public bool isWin;
        public bool isMenuBegin;
        public bool isMenuLvlSelect;

        public bool isGameOld;
        public bool isWinOld;
        public bool isMenuBeginOld;
        public bool isMenuLvlSelectOld;

        public GameProcess()
        {
            isGame = false;
            isWin = false;
            isMenuBegin = false;
            isMenuLvlSelect = false;

            isGameOld = false;
            isWinOld = false;
            isMenuBeginOld = false;
            isMenuLvlSelectOld = false;
        }

        public void NewGame()
        {
            SaveOld();

            isWin = false;
            isGame = false;
            isMenuBegin = true;
            isMenuLvlSelect = false;          
        }

        public void WinGame()
        {
            SaveOld();

            isWin = true;
            isGame = false;
            isMenuBegin = false;
            isMenuLvlSelect = false;
        }

        public void LvlSelect()
        {
            SaveOld();

            isGame = false;
            isWin = false;
            isMenuBegin = false;
            isMenuLvlSelect = true;
        }

        public void StartGame()
        {
            SaveOld();

            isGame = true;
            isWin = false;
            isMenuLvlSelect = false;
            isMenuBegin = false; 
        }

        public void Initialize()
        {
            init();
        }

        public void SaveOld()
        {
            isGameOld = isGame;
            isWinOld = isWin;
            isMenuBeginOld = isMenuBegin;
            isMenuLvlSelectOld = isMenuLvlSelect;
        }
    }
}