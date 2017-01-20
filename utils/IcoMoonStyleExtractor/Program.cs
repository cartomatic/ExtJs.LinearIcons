using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcoMoonStyleExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("need css...");
                return;
            }

            //expect the arg to point to th icomoon.io generated css
            var css = args[0];

            if (!File.Exists(css))
            {
                Console.WriteLine("css file does not exist...");
                return;
            }

            //grab the input file css prefix for the font
            var inCssPrefix = string.Empty;
            if (args.Length >= 2)
                inCssPrefix = args[1];

            var outVarPfx = "pfx";
            if (args.Length >= 3)
                outVarPfx = args[2];

            var outCssPrefix = "csspfx";
            if (args.Length >= 4)
                outCssPrefix = args[3];

            //read the file and extract the stuff...
            //basically lines we're after look like this:
            //.linearicon-home:before {
            //    content: "\e600";
            //}

            var data = new Dictionary<string, string>();

            var className = string.Empty;

            foreach (var line in File.ReadLines(css))
            {
                if (line.Contains(":before"))
                {
                    className =
                        line.Substring(line.IndexOf(".") + 1, line.IndexOf(":") - 1).Replace($"{inCssPrefix}-", "").ToLower().Replace("'", "");
                }

                if (line.Contains("content:"))
                {
                    var content = line.Replace("content:", "").Replace(";", "").Trim();
                    data.Add(className, content);
                }
            }

            var vars = new List<string>();
            var classes = new List<string>();
            var storeData = new List<string>();

            //output vars and css classes
            foreach (var cls in data.Keys)
            {
                //$icon54com-var-add-bag: "\e900";
                vars.Add($"${outVarPfx}-var-{cls}: {data[cls]};");

                //.#{$icon54com-css-prefix}-add-bag:before { content: $icon54com-var-add-bag !important; }
                classes.Add($".#{{${outVarPfx}-css-prefix}}-{cls}:before {{ content: ${outVarPfx}-var-{cls} !important; }}");

                storeData.Add($"{{'name':'{cls}', iconCls: 'x-{outCssPrefix} {outCssPrefix}-{cls}', 'fontCode': '{data[cls]}', 'group': 'group_name'}}");
            }

            var outDir = Path.GetDirectoryName(css);
            File.WriteAllLines(Path.Combine(outDir, "vars.txt"), vars);
            File.WriteAllLines(Path.Combine(outDir, "classes.txt"), classes);
            File.WriteAllText(Path.Combine(outDir, "storedata.txt"), string.Join("," + Environment.NewLine, storeData));
        }
    }
}
