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
    class GameProcess
    {
        public bool isGame;
        public bool isWin;
        public bool isMenuBegin;
        public bool isMenuLvlSelect;

        public GameProcess()
        {
            isGame = false;
            isWin = false;
            isMenuBegin = false;
            isMenuLvlSelect = false;
        }

        public void NewGame()
        {
            isWin = false;
            isGame = false;
            isMenuBegin = true;
            isMenuLvlSelect = false;
        }

        public void WinGame()
        {
            isWin = true;
            isGame = false;
        }

        public void LvlSelect()
        {
            isMenuBegin = false;
            isMenuLvlSelect = true;
        }

        public void StartGame()
        {
            isGame = true;
            isMenuLvlSelect = false;
            isMenuBegin = false;
        }
    }
}