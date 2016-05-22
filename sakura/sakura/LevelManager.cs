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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sakura
{
    [Serializable]
    public class LevelManager
    {
        [Serializable]
        public class LevelItem
        {
            public bool[][] Value;
            public LevelItem()
            {
                Value = new bool[5][];
                for(int i = 0; i < 5; i++)
                {
                    Value[i] = new bool[4];
                }

                for(int i = 0; i < 5; i++)
                {
                    for(int j = 0; j<4; j++)
                    {
                        Value[i][j] = false;
                    }
                }
                Value[0][0] = true;
            }
        }

        public LevelItem Levels;

        public LevelManager ReadLevels()
        {
            try
            {
                var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
                var filePath = System.IO.Path.Combine(sdCardPath + "/Application/Sakura", "sakura.xml");
                FileStream fStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var myBinaryFormatter = new BinaryFormatter();
                var mc = (LevelManager)myBinaryFormatter.Deserialize(fStream);
                fStream.Close();
                return mc;
            }
            catch (Exception e)
            {
                Levels = new LevelItem();
                return this;
            }
        }

        public void WriteLevels()
        {
            var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            if (!Directory.Exists(sdCardPath + "/Application"))
                Directory.CreateDirectory(sdCardPath + "/Application");
            if (!Directory.Exists(sdCardPath + "/Application/Sakura"))
                Directory.CreateDirectory(sdCardPath + "/Application/Sakura");
            var filePath = System.IO.Path.Combine(sdCardPath + "/Application/Sakura", "sakura.xml");
            FileStream fStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            var myBinaryFormatter = new BinaryFormatter();
            myBinaryFormatter.Serialize(fStream, this);
            fStream.Close();
        }
    }
}