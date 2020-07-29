using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Map_Painter
{
    class MinecraftMapColors
    {
        private Color[] flat;
        private Color[] bump_1;
        private Color[] bump_2;
        private string[] block;

        public MinecraftMapColors()
        {
            byte[,] base_colors =
            {
                {127,178,56 },
                {247,233,163 },
                {199,199,199 },
                {255,0,0 },
                {160,160,255 },
                {167,167,167 },
                {0,124,0 },
                {255,255,255 },
                {164,168,184 },
                {151,109,77 },
                {112,112,112 },
                {143,119,72 },
                {255,252,245 },
                {216,127,51 },
                {178,76,216 },
                {102,153,216 },
                {229,229,51 },
                {127,204,25 },
                {242,127,165 },
                {76,76,76 },
                {153,153,153 },
                {76,127,153 },
                {127,63,178 },
                {51,76,178 },
                {102,76,51 },
                {102,127,51 },
                {153,51,51 },
                {25,25,25 },
                {250,238,77 },
                {92,219,213 },
                {74,128,255 },
                {0,217,58 },
                {129,86,49 },
                {112,2,0 },
                {209,177,161 },
                {159,82,36 },
                {149,87,108 },
                {112,108,138 },
                {186,133,36 },
                {103,117,53 },
                {160,77,78 },
                {57,41,35 },
                {137,107,98 },
                {87,92,92 },
                {122,73,88 },
                {76,62,92 },
                {76,50,35 },
                {76,82,42 },
                {142,60,46 },
                {37,22,16 }
            };
            this.block = new string[]
            {
                "草方块，粘液块",
                "沙子，砂岩，白桦木板，末地石，骨块，海龟蛋，脚手架",
                "蜘蛛网，蘑菇柄",
                "红石块，TNT，火",
                "冰",
                "铁块，酿造台，砂轮",
                "树叶，竹子，甘蔗，草，浆果丛，睡莲",
                "雪块，白色羊毛/混凝土(粉末)/染色玻璃/带釉陶瓦/潜影盒",
                "粘土块",
                "泥土，花岗岩，丛林木板，草径，棕色蘑菇方块",
                "原石，安山岩，沙砾，石砖，平滑石头",
                "橡木木板，箱子",
                "闪长岩，石英块，海景灯，白桦木（侧面）",
                "金合欢木板，红沙，陶瓦，南瓜，橙色羊毛/混凝土(粉末)/染色玻璃/带釉陶瓦/潜影盒",
                "紫珀块，品红色羊毛/混凝土(粉末)/染色玻璃/带釉陶瓦/潜影盒",
                "淡蓝色羊毛/混凝土(粉末)/染色玻璃/带釉陶瓦/潜影盒",
                "干草块，海绵，黄色羊毛/混凝土(粉末)/染色玻璃/带釉陶瓦/潜影盒",
                "黄绿色羊毛/混凝土(粉末)/染色玻璃/带釉陶瓦/潜影盒，西瓜",
                "粉色羊毛/混凝土(粉末)/染色玻璃/带釉陶瓦/潜影盒",
                "合金欢木（侧面），灰色羊毛/混凝土(粉末)/染色玻璃/带釉陶瓦/潜影盒，失活的珊瑚块",
                "淡灰色羊毛/混凝土(粉末)/染色玻璃/带釉陶瓦/潜影盒",
                "青色羊毛/混凝土(粉末)/染色玻璃/带釉陶瓦/潜影盒，海晶石",
                "紫色羊毛/混凝土(粉末)/染色玻璃/带釉陶瓦，潜影盒",
                "蓝色羊毛/混凝土(粉末)/染色玻璃/带釉陶瓦/潜影盒",
                "深色橡木木板，灵魂沙，棕色羊毛/混凝土(粉末)/染色玻璃/带釉陶瓦/潜影盒",
                "绿色羊毛/混凝土(粉末)/染色玻璃/带釉陶瓦/潜影盒，干海带块，海泡菜",
                "红色羊毛/混凝土(粉末)/染色玻璃/带釉陶瓦/潜影盒，砖块，红色蘑菇方块，地狱疣块",
                "黑色羊毛/混凝土(粉末)/染色玻璃/带釉陶瓦/潜影盒，煤炭块，黑曜石",
                "金块，轻质压力板，钟",
                "钻石块，信标，海晶石砖，暗海晶石",
                "青金石块",
                "绿宝石块",
                "云杉木板，灰化土",
                "下界岩，下界砖块，岩浆块",
                "白色陶瓦",
                "橙色陶瓦",
                "品红色陶瓦",
                "淡蓝色陶瓦",
                "黄色陶瓦",
                "黄绿色陶瓦",
                "粉红色陶瓦",
                "灰色陶瓦",
                "淡灰色陶瓦",
                "青色陶瓦",
                "紫色陶瓦，紫色潜影盒",
                "蓝色陶瓦",
                "棕色陶瓦",
                "绿色陶瓦",
                "红色陶瓦",
                "黑色陶瓦"
            };
            this.flat = new Color[50];
            this.bump_1 = new Color[50];
            this.bump_2 = new Color[50];
            for(int i = 0; i < 50; i++)
            {
                this.bump_1[i] = Color.FromRgb(base_colors[i, 0], base_colors[i, 1], base_colors[i, 2]);
                this.flat[i] = Color.FromRgb((byte)((double)(base_colors[i, 0])/255.0*220.0), (byte)((base_colors[i, 1]) / 255.0 * 220.0), (byte)((base_colors[i, 2]) / 255.0 * 220.0));
                this.bump_2[i] = Color.FromRgb((byte)((double)(base_colors[i, 0]) / 255.0 * 180.0), (byte)((base_colors[i, 1]) / 255.0 * 180.0), (byte)((base_colors[i, 2]) / 255.0 * 180.0));
            }
        }

        public Color FindColor(byte R, byte G, byte B, bool flat, out string info)
        {
            double distance = 200000.0;
            double disbuf = 0;
            Color c = new Color();
            int info_id = 0;
            info = "未找到颜色";
            foreach(Color color in this.flat)
            {
                disbuf = ((int)R - (int)color.R) * ((int)R - (int)color.R) + ((int)G - (int)color.G) * ((int)G - (int)color.G) + ((int)B - (int)color.B) * ((int)B - (int)color.B);
                //disbuf = HSV_distance(R, G, B, color.R, color.G, color.B);
                if (disbuf < distance)
                {
                    distance = disbuf;
                    c = color;
                    info = this.block[info_id];
                }
                info_id++;
            }
            if (!flat)
            {
                info_id = 0;
                foreach (Color color in this.bump_1)
                {
                    disbuf = ((int)R - (int)color.R) * ((int)R - (int)color.R) + ((int)G - (int)color.G) * ((int)G - (int)color.G) + ((int)B - (int)color.B) * ((int)B - (int)color.B);
                    //disbuf = HSV_distance(R, G, B, color.R, color.G, color.B);
                    if (disbuf < distance)
                    {
                        distance = disbuf;
                        c = color;
                        info = "（高于北边一格方块）"+this.block[info_id];
                    }
                    info_id++;
                }
                info_id = 0;
                foreach (Color color in this.bump_2)
                {
                    disbuf = ((int)R - (int)color.R) * ((int)R - (int)color.R) + ((int)G - (int)color.G) * ((int)G - (int)color.G) + ((int)B - (int)color.B) * ((int)B - (int)color.B);
                    //disbuf = HSV_distance(R, G, B, color.R, color.G, color.B);
                    if (disbuf < distance)
                    {
                        distance = disbuf;
                        c = color;
                        info = "（低于北边一格方块）" + this.block[info_id];
                    }
                    info_id++;
                }
            }
            return c;
        }

        /*
        private double HSV_distance(byte R, byte G, byte B, byte R1, byte G1, byte B1)
        {
            byte Cmax = R > G ? (R > B ? R : B) : (G > B ? G : B);
            byte Cmin = R < G ? (R < B ? R : B) : (G < B ? G : B);
            byte Cmax1 = R1 > G1 ? (R1 > B1 ? R1 : B1) : (G1 > B1 ? G1 : B1);
            byte Cmin1 = R1 < G1 ? (R1 < B1 ? R1 : B1) : (G1 < B1 ? G1 : B1);
            double H, H1, S, S1, V = Cmax / 255.0, V1 = Cmax1 / 255.0, x, x1, y, y1, z, z1;
            if (Cmax - Cmin == 0)
            {
                H = 0;
            }
            else if (Cmax == R)
            {
                H = 60 * ((G - B) / (Cmax - Cmin) + 0);
            }
            else if(Cmax == G)
            {
                H = 60 * ((B - R) / (Cmax - Cmin) + 2);
            }
            else
            {
                H = 60 * ((R - G) / (Cmax - Cmin) + 4);
            }
            if (Cmax == 0)
            {
                S = 0;
            }
            else
            {
                S = (Cmax - Cmin) / Cmax;
            }
            if (Cmax1 - Cmin1 == 0)
            {
                H1 = 0;
            }
            else if (Cmax1 == R1)
            {
                H1 = 60 * ((G1 - B1) / (Cmax1 - Cmin1) + 0);
            }
            else if (Cmax1 == G1)
            {
                H1 = 60 * ((B1 - R1) / (Cmax1 - Cmin1) + 2);
            }
            else
            {
                H1 = 60 * ((R1 - G1) / (Cmax1 - Cmin1) + 4);
            }
            if (Cmax1 == 0)
            {
                S1 = 0;
            }
            else
            {
                S1 = (Cmax1 - Cmin1) / Cmax1;
            }
            x = 10 * V * S * Math.Cos(H);
            x1 = 10 * V1 * S1 * Math.Cos(H1);
            y = 10 * V * S * Math.Sin(H);
            y1 = 10 * V1 * S1 * Math.Sin(H1);
            z = 10 * (1 - V);
            z1 = 10 * (1 - V);
            double distance = (double)Math.Sqrt((x - x1) * (x - x1) + (y - y1) * (y - y1) + (z - z1) * (z - z1));
            return distance;
        }
        */
    }
}
