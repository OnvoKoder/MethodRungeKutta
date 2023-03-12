using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RungeKutte4rdOrder.Class.Helper
{
    class Helper
    { 
        private readonly string file = @"\Help\Help.txt";
        private string GetPath()
        {
            string[] path = Directory.GetCurrentDirectory().Split(new string[] { "bin" }, StringSplitOptions.None);
            if (!File.Exists(path[0] + file)) File.Create(path[0] + file).Close();
            return path[0] + file;
        }
        internal void GetHelper(out List<string> helper)
        {
            helper = new List<string>();
            for (int i = 0; i < File.ReadAllLines(GetPath()).Count(); i++)
                helper.Add(File.ReadAllLines(GetPath()).ElementAt(i));
        }
    }
}
