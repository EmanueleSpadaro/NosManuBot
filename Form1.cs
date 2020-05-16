using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

using System.Diagnostics; //Test
using System.Runtime.InteropServices; //Test

namespace ManuBot
{
    public partial class Form1 : Form
    {
        private Dictionary<IntPtr, int> botList = new Dictionary<IntPtr, int>();
        public enum KeyCodes
        {
            #region KeyCodes
            VK_PRIOR = 0x21,
            VK_NEXT = 0x22,
            VK_END = 0x23,
            VK_HOME = 0x24,

            VK_LEFT = 0x25,
            VK_UP = 0x26,
            VK_RIGHT = 0x27,
            VK_DOWN = 0x28,

            VK_CANCEL = 0x03,
            VK_BACK = 0x08,
            VK_TAB = 0x09,
            VK_RETURN = 0x0D,
            VK_SHIFT = 0x10,
            VK_CONTROL = 0x11,
            VK_MENU = 0x12,
            VK_PAUSE = 0x13,
            VK_CAPITAL = 0x14,
            VK_ESCAPE = 0x1B,
            VK_SPACE = 0x20,
            VK_SNAPSHOT = 0x2C,
            VK_INSERT = 0x2D,
            VK_DELETE = 0x2E,
            VK_LWIN = 0x5B,
            VK_RWIN = 0x5C,
            VK_NUMPAD0 = 0x60,
            VK_NUMPAD1 = 0x61,
            VK_NUMPAD2 = 0x62,
            VK_NUMPAD3 = 0x63,
            VK_NUMPAD4 = 0x64,
            VK_NUMPAD5 = 0x65,
            VK_NUMPAD6 = 0x66,
            VK_NUMPAD7 = 0x67,
            VK_NUMPAD8 = 0x68,
            VK_NUMPAD9 = 0x69,
            VK_MULTIPLY = 0x6A,
            VK_ADD = 0x6B,
            VK_SUBTRACT = 0x6D,
            VK_DECIMAL = 0x6E,
            VK_DIVIDE = 0x6F,
            VK_F1 = 0x70,
            VK_F2 = 0x71,
            VK_F3 = 0x72,
            VK_F4 = 0x73,
            VK_F5 = 0x74,
            VK_F6 = 0x75,
            VK_F7 = 0x76,
            VK_F8 = 0x77,
            VK_F9 = 0x78,
            VK_F10 = 0x79,
            VK_F11 = 0x7A,
            VK_F12 = 0x7B,
            VK_NUMLOCK = 0x90,
            VK_SCROLL = 0x91,
            VK_ALT = 0x12,

            // Regular

            VK_0 = 0x30,
            VK_1 = 0x31,
            VK_2 = 0x32,
            VK_3 = 0x33,
            VK_4 = 0x34,
            VK_5 = 0x35,
            VK_6 = 0x36,
            VK_7 = 0x37,
            VK_8 = 0x38,
            VK_9 = 0x39,

            VK_A = 0x41,
            VK_B = 0x42,
            VK_C = 0x43,
            VK_D = 0x44,
            VK_E = 0x45,
            VK_F = 0x46,
            VK_G = 0x47,
            VK_H = 0x48,
            VK_I = 0x49,
            VK_J = 0x4A,
            VK_K = 0x4B,
            VK_L = 0x4C,
            VK_M = 0x4D,
            VK_N = 0x4E,
            VK_O = 0x4F,
            VK_P = 0x50,
            VK_Q = 0x51,
            VK_R = 0x52,
            VK_S = 0x53,
            VK_T = 0x54,
            VK_U = 0x55,
            VK_V = 0x56,
            VK_W = 0x57,
            VK_X = 0x58,
            VK_Y = 0x59,
            VK_Z = 0x5A,

            VK_APPS = 0x5D,
            VK_SLEEP = 0x5F,
            VK_SEPERATOR = 0x6C,
            VK_LSHIFT = 0xA0,
            VK_RSHIFT = 0xA1,
            VK_LCONTROL = 0xA2,
            VK_RCOONTROL = 0xA3,
            VK_LMENU = 0xA4,
            VK_RMENU = 0xA5,
            #endregion
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void TestBtn_Click(object sender, EventArgs e)
        {
            string msg = "";
            for (int i = 0; i < ProcessManager.NostaleWindowHandles.Count; i++)
            {
                msg += $"{i}->{ProcessManager.NostaleWindowHandles[i].ToString()}\n";
            }

            var scelto = Convert.ToInt32(Interaction.InputBox(msg, "Scegli client"));

            var hWnd = ProcessManager.NostaleWindowHandles[scelto];

            this.ClientNumberLabel.Text += " " + hWnd;

            var quantita = Convert.ToInt32(Interaction.InputBox(msg, "Quantità minigames?", "20"));

            //Task.Run(() => ReopenMinigame(hWnd));
            //Task.Run(() => SearchForNosVerde(hWnd));
            //Task.Run(() => PointTester(hWnd));

            Task.Run(() => StartFishing(hWnd, quantita));

        }

        private async Task SearchForNosVerde(IntPtr hWnd)
        {
            while (true)
            {
                //Stopwatch sw = new Stopwatch();
                //sw.Start();

                var bMap = ProcessManager.CaptureWindow(hWnd);
                var point = ProcessManager.Find(bMap, Properties.Resources.FrecciaInizio1s);
                //sw.Stop();
                if (point.HasValue)
                {
                    //Debug.WriteLine($"[Cycle time {sw.Elapsed}] Found @ {point.Value}");
                    Debug.WriteLine($"[Cycle time] Found @ {point.Value}");
                    //ProcessManager.SendClick(hWnd, point.Value.X, point.Value.Y);
                    ProcessManager.SendClick(hWnd, point.Value.X, point.Value.Y + 50);
                    break;
                }
                await Task.Delay(50);
            }
        }

        private async Task PointTester(IntPtr hWnd)
        {
            int colore = 0;
            while(true)
            {
                var bMap = ProcessManager.CaptureWindow(hWnd);
                var memMap = ProcessManager.GetPixelArray(bMap);

                var color = ProcessManager.GetPixelColor(memMap, 663, 419);
                if(colore != color)
                {
                    colore = color;
                    Debug.WriteLine($"[Point Tester] 345, 435 = {color}");
                }
                await Task.Delay(50);
            }
        }
        private async Task StartFishing(IntPtr hWnd, int times = 20)
        {
            Debug.WriteLine("[StartFishing] Method Started");
            await SearchAndClickStart(hWnd);
            bool fishReady = false, fishGetBounty = false;
            int randomOver = ProcessManager.Random(800, 1300), randomExit = ProcessManager.Random(4400, 5500), random20k = ProcessManager.Random(800, 1300);

            int holdTime = 5;

            //395, 369 Pos SX goccia
            Point GocciaSX = new Point() { X = 395, Y = 369 };
            int GocciaSXColor = -3737601;
            Point NeroSX = new Point() { X = GocciaSX.X - 10, Y = GocciaSX.Y + 8 }; //385, 377
            int NeroSXColor = -14153976;

            Point GocciaUP = new Point() { X = 548, Y = 323 };
            int GocciaUPColor = -3737601;
            Point NeroUP = new Point() { X = 503, Y = 342 };
            int NeroUPColor = -12975614;

            Point GocciaBT = new Point() { X = 509, Y = 429 };
            int GocciaBTColor = -3737601;
            Point NeroBT = new Point() { X = 465, Y = 448 };
            int NeroBTColor = -15658221;

            Point GocciaDX = new Point() { X = 666, Y = 372 };
            int GocciaDXColor = -3737601;
            Point NeroDX = new Point() { X = 623, Y = 390 };
            int NeroDXColor = -15658221;

            Point FishEventPoint = new Point() { X = 724, Y = 384 };
            int FishEventColor = -16645630;

            Point getReward = new Point() { X = 663, Y = 429 };
            int purpleGetReward = -6989917; //Quando ha questo colore basta cliccare nella posizione due volte teoricamente, ma controlliamo se è lvl 5
                                            //int Livello5Color = -10901800; //Se getReward è di questo colore dopo il primo click sul purple, allora è un livello 5 [Inutile, basta doppio click]
                                            //Inoltre dobbiamo controllare che sia livello 5 prima di cliccare sul reward, sennò sprechiamo punti [Find RewardCheck.bmp]

            Point RetryButton = new Point() { X = 345, Y = 435 };
            int RetryButtonColor = -13339202; //Se il punto retry button sarà di questo colore, allora il livello si è concluso
            bool LivelloCompletato = false;

            Point RetryAfterBounty = new Point() { X = 422, Y = 465 }; //Punto per riprovare a giocare dopo aver riscattato una ricompensa

            Point AnnullaButton = new Point() { X = 663, Y = 419 }; //Se AnnullaButton è di questo colore, allora siamo senza punti e dobbiamo Couponizzare
            int AnnullaButtonColor = -14851911;
            Point StopButton = new Point() { X = 588, Y = 466 }; //Il pulsante dopo l'annulla button per uscire dal minigame

            Point FrecciaTraTende = new Point() { X = 511, Y = 462 };
            Point ApriButton = new Point() { X = 511, Y = 511 }; //Il bottone della tendina che si apre cliccando la freccia [Double click]



            int fishCounter = 0;

            int completedGames = 0;
            int previousGames = -1;
            int attempts = 0;

            int toComplete = times;
            while (true && completedGames != toComplete)
            {
                if (completedGames != previousGames)
                {
                    Debug.WriteLine($"Minigames Done: {completedGames}/{toComplete}");
                    Debug.WriteLine("Attempts: " + attempts);
                    previousGames = completedGames;
                }
                var currentbMap = ProcessManager.CaptureWindow(hWnd);
                var currentMemMap = ProcessManager.GetPixelArray(currentbMap);
                int currentSXColor = ProcessManager.GetPixelColor(currentMemMap, GocciaSX.X, GocciaSX.Y);
                int currentUPColor = ProcessManager.GetPixelColor(currentMemMap, GocciaUP.X, GocciaUP.Y);
                int currentBTColor = ProcessManager.GetPixelColor(currentMemMap, GocciaBT.X, GocciaBT.Y);
                int currentDXColor = ProcessManager.GetPixelColor(currentMemMap, GocciaDX.X, GocciaDX.Y);
                int isFishEventColor = ProcessManager.GetPixelColor(currentMemMap, FishEventPoint.X, FishEventPoint.Y);

                int currentSXBlack = ProcessManager.GetPixelColor(currentMemMap, NeroSX.X, NeroSX.Y);
                int currentUPBlack = ProcessManager.GetPixelColor(currentMemMap, NeroUP.X, NeroUP.Y);
                int currentBTBlack = ProcessManager.GetPixelColor(currentMemMap, NeroBT.X, NeroBT.Y);
                int currentDXBlack = ProcessManager.GetPixelColor(currentMemMap, NeroDX.X, NeroDX.Y);

                if (ProcessManager.Find(currentbMap, Properties.Resources.ShouldBe20k).HasValue)
                {
                    LivelloCompletato = true;
                    switch (ProcessManager.Random(1, 4))
                    {
                        case 1:
                            {
                                await ProcessManager.SendKey(hWnd, ProcessManager.KeyCodes.VK_LEFT, ProcessManager.Random(200, 300));
                                break;
                            }
                        case 2:
                            {
                                await ProcessManager.SendKey(hWnd, ProcessManager.KeyCodes.VK_UP, ProcessManager.Random(200, 300));
                                break;
                            }
                        case 3:
                            {
                                await ProcessManager.SendKey(hWnd, ProcessManager.KeyCodes.VK_DOWN, ProcessManager.Random(200, 300));
                                break;
                            }
                        case 4:
                            {
                                await ProcessManager.SendKey(hWnd, ProcessManager.KeyCodes.VK_RIGHT, ProcessManager.Random(200, 300));
                                break;
                            }
                    }

                    await Task.Delay(ProcessManager.Random(75, 100));
                }

                //Controlliamo se è possibile riscuotere un reward
                int currentGetReward = ProcessManager.GetPixelColor(currentMemMap, getReward.X, getReward.Y);
                if (currentGetReward == purpleGetReward && LivelloCompletato)
                {
                    attempts++;
                    completedGames++;
                    //this.dataGridView1.Rows[myId].Cells[1].Value = completedGames.ToString();
                    await Task.Delay(450);
                    ProcessManager.SendClick(hWnd, getReward.X, getReward.Y);
                    await Task.Delay(1200);
                    ProcessManager.SendClick(hWnd, getReward.X, getReward.Y);


                    LivelloCompletato = false; //Resettiamo la variabile di completamento

                    await Task.Delay(1000);
                    ProcessManager.SendClick(hWnd, RetryAfterBounty.X, RetryAfterBounty.Y);
                    await Task.Delay(1000);

                    currentbMap = ProcessManager.CaptureWindow(hWnd);
                    currentMemMap = ProcessManager.GetPixelArray(currentbMap);

                    int hasNoPoints = ProcessManager.GetPixelColor(currentMemMap, AnnullaButton.X, AnnullaButton.Y); //Se spunta il bottone di annullamento siamo senza punti
                    if(hasNoPoints == AnnullaButtonColor)
                    {
                        //Clicchiamo su annulla
                        ProcessManager.SendClick(hWnd, AnnullaButton.X, AnnullaButton.Y);
                        await Task.Delay(250);
                        ProcessManager.SendClick(hWnd, StopButton.X, StopButton.Y);
                        await Task.Delay(250);
                        await Couponize(hWnd);
                        await Task.Delay(250);
                        await ReopenMinigame(hWnd);
                    }

                    Debug.WriteLine("[CurrentGetReward} Ran Successfully, calling for new game");
                    await Task.Delay(1000); //Aspettiamo 1s per startare il nuovo game, non si sa mai
                    await SearchAndClickStart(hWnd, doOnce: true);
                    continue;
                }

                int currentRetry = ProcessManager.GetPixelColor(currentMemMap, RetryButton.X, RetryButton.Y);

                //Fallito, ricominciamo
                if (currentRetry == RetryButtonColor && fishCounter != 0)
                {
                    attempts++;
                    Debug.WriteLine("Fallito, retry");
                    await Task.Delay(450);
                    ProcessManager.SendClick(hWnd, RetryButton.X, RetryButton.Y);
                    await Task.Delay(1000); //Aspettiamo almeno 1s per restartare il game, non si sa mai
                    fishCounter = 0;
                    await SearchAndClickStart(hWnd, doOnce: true);
                }





                //EOF Debugging Black Fish Zone
                if (currentSXColor == GocciaSXColor && currentSXBlack != NeroSXColor)
                {
                    await Task.Delay(100);
                    await ProcessManager.SendKey(hWnd, ProcessManager.KeyCodes.VK_LEFT, holdTime);
                    await Task.Delay(ProcessManager.Delay / 2);
                    await Task.Delay(ProcessManager.Delay / 2);

                    fishCounter++;

                    //currentbMap.Save(Application.StartupPath + $@"\SX{SXCtr++}.bmp");
                }
                else if (currentBTColor == GocciaBTColor && currentBTBlack != NeroBTColor)
                {
                    await Task.Delay(100);
                    await ProcessManager.SendKey(hWnd, ProcessManager.KeyCodes.VK_DOWN, holdTime);
                    await Task.Delay(ProcessManager.Delay / 2);
                    await Task.Delay(ProcessManager.Delay / 2);

                    fishCounter++;
                }
                else if (currentUPColor == GocciaUPColor && currentUPBlack != NeroUPColor)
                {
                    await Task.Delay(100);
                    await ProcessManager.SendKey(hWnd, ProcessManager.KeyCodes.VK_UP, holdTime);
                    await Task.Delay(ProcessManager.Delay / 2);
                    await Task.Delay(ProcessManager.Delay / 2);

                    fishCounter++;
                    //currentbMap.Save(Application.StartupPath + $@"\SX{SXCtr++}.bmp");
                }
                else if (currentDXColor == GocciaDXColor && currentDXBlack != NeroDXColor)
                {
                    await Task.Delay(100);
                    await ProcessManager.SendKey(hWnd, ProcessManager.KeyCodes.VK_RIGHT, holdTime);
                    await Task.Delay(ProcessManager.Delay / 2);
                    await Task.Delay(ProcessManager.Delay / 2);

                    fishCounter++;
                }

                if (isFishEventColor == FishEventColor)
                {
                    await FishEvent(hWnd);
                }
                await Task.Delay(75);
            }

            Debug.WriteLine("End of Minigames");
        }






        private async Task SearchAndClickStart(IntPtr hWnd, bool doOnce = false)
        {
            //Cerca all'infinito il bottone per startare il minigame
            Point? point = null;
            Debug.WriteLine("[SearchAndClickStart]Searching for Start Button");
            while (true)
            {
                point = ProcessManager.Find(ProcessManager.CaptureWindow(hWnd), Properties.Resources.StartFishpond);
                if (point.HasValue)
                    break;
                if (doOnce)
                    break;
            }
            await Task.Delay(ProcessManager.Random(10, 400)); //Script Line 966
            if(point.HasValue)
                ProcessManager.SendClick(hWnd, point.Value.X+ProcessManager.Random(0, 120), point.Value.Y+ProcessManager.Random(16,25)); //Script Line 967
            Debug.WriteLine("[SearchAndClickStart]Click sent");
        }

        private async Task FishEvent(IntPtr hWnd)
        {
            Point FishEventPoint = new Point() { X = 724, Y = 384 };
            int FishEventColor = -16645630;
            while (true)
            {
                var currentbMap = ProcessManager.CaptureWindow(hWnd);
                var membMap = ProcessManager.GetPixelArray(currentbMap);
                int isFishEventColor = ProcessManager.GetPixelColor(membMap, FishEventPoint.X, FishEventPoint.Y);

                if(isFishEventColor != FishEventColor)
                {
                    //Debug.WriteLine("[FISH EVENT] Fish Event end detected, exiting loop");
                    break;
                }

                Point? point = ProcessManager.Find(currentbMap, Properties.Resources.arrow_up);
                if (point.HasValue)
                {
                    await ProcessManager.SendKey(hWnd, ProcessManager.KeyCodes.VK_UP);
                    await Task.Delay(50);
                    continue;
                }
                point = ProcessManager.Find(currentbMap, Properties.Resources.arrow_down);
                if(point.HasValue)
                {
                    await ProcessManager.SendKey(hWnd, ProcessManager.KeyCodes.VK_DOWN);
                    await Task.Delay(50);
                    continue;
                }
                point = ProcessManager.Find(currentbMap, Properties.Resources.arrow_left);
                if (point.HasValue)
                {
                    await ProcessManager.SendKey(hWnd, ProcessManager.KeyCodes.VK_LEFT);
                    await Task.Delay(50);
                    continue;
                }
                point = ProcessManager.Find(currentbMap, Properties.Resources.arrow_right);
                if (point.HasValue)
                {
                    await ProcessManager.SendKey(hWnd, ProcessManager.KeyCodes.VK_RIGHT);
                    await Task.Delay(50);
                    continue;
                }
            }
        }
        
        private async Task Couponize(IntPtr hWnd)
        {
            for(int i = 0; i < 2000; i+=500)
            {
                await ProcessManager.SendKey(hWnd, ProcessManager.KeyCodes.VK_0, 50);
                await Task.Delay(250);
                await ProcessManager.SendKey(hWnd, ProcessManager.KeyCodes.VK_RETURN);
                await Task.Delay(250);

            }
        }

        private async Task ReopenMinigame(IntPtr hWnd)
        {
            while (true)
            {
                Debug.WriteLine("[ReopenMinigame] Attempt to open");
                var bMap = ProcessManager.CaptureWindow(hWnd);
                var point = ProcessManager.Find(bMap, Properties.Resources.FrecciaInizio1);
                if (point.HasValue)
                {
                    ProcessManager.SendClick(hWnd, point.Value.X, point.Value.Y);
                    await Task.Delay(450);
                    ProcessManager.SendClick(hWnd, point.Value.X, point.Value.Y + 50);
                    await Task.Delay(450);
                    ProcessManager.SendClick(hWnd, point.Value.X, point.Value.Y + 50);
                    break;
                }
                point = ProcessManager.Find(bMap, Properties.Resources.FrecciaInizio1s);
                if (point.HasValue)
                {
                    //ProcessManager.SendClick(hWnd, point.Value.X, point.Value.Y);
                    //await Task.Delay(50);
                    ProcessManager.SendClick(hWnd, point.Value.X, point.Value.Y + 50);
                    Debug.WriteLine("Click one");
                    await Task.Delay(450);
                    ProcessManager.SendClick(hWnd, point.Value.X, point.Value.Y + 50);
                    Debug.WriteLine("Click two");
                    break;
                }
                await Task.Delay(50);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
