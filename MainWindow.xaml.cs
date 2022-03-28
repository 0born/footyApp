using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;
using System.Timers;

namespace FootyScores
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// team1 and team2 names in txt file 
    /// separate txt for goals/behinds/total.
    public partial class MainWindow : Window
    {
        //initialize timer display
        private Stopwatch stopwatch;
        private Timer timer;
        const string startTimeDisplay = "00:00";
        
        public MainWindow()
        {
            InitializeComponent();
            TimeDisplay.Content = startTimeDisplay;

            stopwatch = new Stopwatch();
            timer = new Timer(interval:1000);

            timer.Elapsed += OnTimerElapse;

            //Write initial qtr/time to txt
            File.WriteAllText(@"C:\\demos\\footyTimer.txt", qtrDisplay.Content + "\n" + startTimeDisplay);
        }

        private void OnTimerElapse(object sender, ElapsedEventArgs e)
        {
            //for every second, update time in txt file
            Application.Current.Dispatcher.Invoke(() => TimeDisplay.Content = stopwatch.Elapsed.ToString(format: @"mm\:ss"));
            Application.Current.Dispatcher.Invoke(() => File.WriteAllText(@"C:\\demos\\footyTimer.txt", qtrDisplay.Content + "\n" + TimeDisplay.Content.ToString()));
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            //start timer when start button is clicked, disable clear button
            stopwatch.Start();
            timer.Start();
            clear.IsEnabled = false;
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            //stop the timer when stop button is clicked, enable clear button
            stopwatch.Stop();
            timer.Stop();
            clear.IsEnabled = true;
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            //reset the timer and update txt file when clear button is clicked
            stopwatch.Reset();
            TimeDisplay.Content = startTimeDisplay;
            File.WriteAllText(@"C:\\demos\\footyTimer.txt", qtrDisplay.Content + "\n" + startTimeDisplay);
        }

        private void saveNames_Click(object sender, RoutedEventArgs e)
        {
            writeToTextFile();
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            goalTotal1.Content = "  0";
            goalTotal2.Content = "  0";           
            behindTotal1.Content = "  0";          
            behindTotal2.Content = "  0";       
            total1.Content = "  0";
            total2.Content = "  0";

            writeToTextFile();
        }

        private void writeToTextFile()
        {
            /*/ one file for all scores
            TextWriter txt = new StreamWriter("C:\\demos\\footyScores.txt");
            
            string scores1 = String.Format("{0, -2} {1, -2}  {2}\n", goalTotal1.Content, behindTotal1.Content, total1.Content);
            string scores2 = String.Format("{0, -2} {1, -2}  {2}", goalTotal2.Content, behindTotal2.Content, total2.Content);

            txt.Write(scores1);
            txt.Write(scores2);
            txt.Close();
            */

            //two files for team totalScores
            TextWriter txtTotal1 = new StreamWriter("C:\\demos\\team1Total.txt");
            TextWriter txtTotal2 = new StreamWriter("C:\\demos\\team2Total.txt");
            txtTotal1.Write(total1.Content);
            txtTotal2.Write(total2.Content);
            txtTotal1.Close();
            txtTotal2.Close();

            // two files for the scores
            TextWriter txt1 = new StreamWriter("C:\\demos\\footyScores1.txt");
            TextWriter txt2 = new StreamWriter("C:\\demos\\footyScores2.txt");

            string scores1 = String.Format("{0, -2} {1, -2}  {2}", goalTotal1.Content, behindTotal1.Content, total1.Content);
            string scores2 = String.Format("{0, -2} {1, -2}  {2}", goalTotal2.Content, behindTotal2.Content, total2.Content);

            txt1.Write(scores1);
            txt2.Write(scores2);
            txt1.Close();
            txt2.Close();

            //save team names
            StreamWriter teamName1 = new StreamWriter("C:\\demos\\teamName1.txt");
            StreamWriter teamName2 = new StreamWriter("C:\\demos\\teamName2.txt");
            teamName1.WriteLine(team1Name.Text);
            teamName2.WriteLine(team2Name.Text);
            teamName1.Close();
            teamName2.Close();

        }

        private void goal1_Click(object sender, RoutedEventArgs e)
        {

            // Convert current goal total to integer, increment and update label while adjusting spacing
            int totalGoals = Convert.ToInt32(goalTotal1.Content);
            totalGoals++;
            if(totalGoals < 10)
            {
                String goals = "  " + totalGoals.ToString();
                goalTotal1.Content = goals;
            } else if(totalGoals >= 10 && totalGoals < 100)
            {
                String goals = " " + totalGoals.ToString();
                goalTotal1.Content = goals;
            } else
            {
                goalTotal1.Content = totalGoals.ToString();
            }
            

            //Convert total score to integer, add 6 points and update label
            int totalScore = Convert.ToInt32(total1.Content);
            totalScore = totalScore + 6;
            if(totalScore < 10)
            {
                String total = "  " + totalScore.ToString();
                total1.Content = total;
            } else if(totalScore >= 10 && totalScore < 100)
            {
                String total = " " + totalScore.ToString();
                total1.Content = total;
            } else
            {
                total1.Content = totalScore.ToString();
            }
            

            writeToTextFile();


        }
        private void minusGoal1_Click(object sender, RoutedEventArgs e)
        {

            int totalGoals = Convert.ToInt32(goalTotal1.Content);
            totalGoals--;
            if (totalGoals < 10)
            {
                String goals = "  " + totalGoals.ToString();
                goalTotal1.Content = goals;
            }
            else if (totalGoals >= 10 && totalGoals < 100)
            {
                String goals = " " + totalGoals.ToString();
                goalTotal1.Content = goals;
            }
            else
            {
                goalTotal1.Content = totalGoals.ToString();
            }

            //Convert total score to integer, minus 6 points and update label
            int totalScore = Convert.ToInt32(total1.Content);
            totalScore = totalScore - 6;
            if (totalScore < 10)
            {
                String total = "  " + totalScore.ToString();
                total1.Content = total;
            }
            else if (totalScore >= 10 && totalScore < 100)
            {
                String total = " " + totalScore.ToString();
                total1.Content = total;
            }
            else
            {
                total1.Content = totalScore.ToString();
            }

            writeToTextFile();
        }

        private void behind_button1_Click(object sender, RoutedEventArgs e)
        {
          
            int totalBehinds = Convert.ToInt32(behindTotal1.Content);
            totalBehinds++;
            if (totalBehinds < 10)
            {
                String behinds = "  " + totalBehinds.ToString();
                behindTotal1.Content = behinds;
            }
            else if (totalBehinds >= 10 && totalBehinds < 100)
            {
                String behinds = " " + totalBehinds.ToString();
                behindTotal1.Content = behinds;
            }
            else
            {
                behindTotal1.Content = totalBehinds.ToString();
            }
            

            //Convert total score to integer, add 1 point and update label
            int totalScore = Convert.ToInt32(total1.Content);
            totalScore++;
            if (totalScore < 10)
            {
                String total = "  " + totalScore.ToString();
                total1.Content = total;
            }
            else if (totalScore >= 10 && totalScore < 100)
            {
                String total = " " + totalScore.ToString();
                total1.Content = total;
            }
            else
            {
                total1.Content = totalScore.ToString();
            }

            writeToTextFile();
        }

        private void minusBehind1_Click(object sender, RoutedEventArgs e)
        {
            
            int totalBehinds = Convert.ToInt32(behindTotal1.Content);
            totalBehinds--;
            if (totalBehinds < 10)
            {
                String behinds = "  " + totalBehinds.ToString();
                behindTotal1.Content = behinds;
            }
            else if (totalBehinds >= 10 && totalBehinds < 100)
            {
                String behinds = " " + totalBehinds.ToString();
                behindTotal1.Content = behinds;
            }
            else
            {
                behindTotal1.Content = totalBehinds.ToString();
            }

            //Convert total score to integer, minus 1 point and update label
            int totalScore = Convert.ToInt32(total1.Content);
            totalScore--;
            if (totalScore < 10)
            {
                String total = "  " + totalScore.ToString();
                total1.Content = total;
            }
            else if (totalScore >= 10 && totalScore < 100)
            {
                String total = " " + totalScore.ToString();
                total1.Content = total;
            }
            else
            {
                total1.Content = totalScore.ToString();
            }

            writeToTextFile();
        }
        
        private void goal_button2_Click(object sender, RoutedEventArgs e)
        {
            //Convert current goal total to integer, increment and update label
            int totalGoals = Convert.ToInt32(goalTotal2.Content);
            totalGoals++;
            if (totalGoals < 10)
            {
                String goals = "  " + totalGoals.ToString();
                goalTotal2.Content = goals;
            }
            else if (totalGoals >= 10 && totalGoals < 100)
            {
                String goals = " " + totalGoals.ToString();
                goalTotal2.Content = goals;
            }
            else
            {
                goalTotal2.Content = totalGoals.ToString();
            }

            //Convert total score to integer, add 6 points and update label
            int totalScore = Convert.ToInt32(total2.Content);
            totalScore = totalScore + 6;
            if (totalScore < 10)
            {
                String total = "  " + totalScore.ToString();
                total2.Content = total;
            }
            else if (totalScore >= 10 && totalScore < 100)
            {
                String total = " " + totalScore.ToString();
                total2.Content = total;
            }
            else
            {
                total2.Content = totalScore.ToString();
            }

            writeToTextFile();
        }

        private void minusGoal2_Click(object sender, RoutedEventArgs e)
        {
            //Convert current goal total to integer, decrement and update label
            int totalGoals = Convert.ToInt32(goalTotal2.Content);
            totalGoals--;
            if (totalGoals < 10)
            {
                String goals = "  " + totalGoals.ToString();
                goalTotal2.Content = goals;
            }
            else if (totalGoals >= 10 && totalGoals < 100)
            {
                String goals = " " + totalGoals.ToString();
                goalTotal2.Content = goals;
            }
            else
            {
                goalTotal2.Content = totalGoals.ToString();
            }

            //Convert total score to integer, minus 6 points and update label
            int totalScore = Convert.ToInt32(total2.Content);
            totalScore = totalScore - 6;
            if (totalScore < 10)
            {
                String total = "  " + totalScore.ToString();
                total2.Content = total;
            }
            else if (totalScore >= 10 && totalScore < 100)
            {
                String total = " " + totalScore.ToString();
                total2.Content = total;
            }
            else
            {
                total2.Content = totalScore.ToString();
            }

            writeToTextFile();
        }

        private void behind_button2_Click(object sender, RoutedEventArgs e)
        {
            //Convert total behinds to integer, increment and update label
            int totalBehinds = Convert.ToInt32(behindTotal2.Content);
            totalBehinds++;
            if (totalBehinds < 10)
            {
                String behinds = "  " + totalBehinds.ToString();
                behindTotal2.Content = behinds;
            }
            else if (totalBehinds >= 10 && totalBehinds < 100)
            {
                String behinds = " " + totalBehinds.ToString();
                behindTotal2.Content = behinds;
            }
            else
            {
                behindTotal2.Content = totalBehinds.ToString();
            }

            //Convert total score to integer, add 1 point and update label
            int totalScore = Convert.ToInt32(total2.Content);
            totalScore++;
            if (totalScore < 10)
            {
                String total = "  " + totalScore.ToString();
                total2.Content = total;
            }
            else if (totalScore >= 10 && totalScore < 100)
            {
                String total = " " + totalScore.ToString();
                total2.Content = total;
            }
            else
            {
                total2.Content = totalScore.ToString();
            }

            writeToTextFile();
        }

        private void minusBehind2_Click(object sender, RoutedEventArgs e)
        {
            //Convert total behinds to integer, decrement and update label
            int totalBehinds = Convert.ToInt32(behindTotal2.Content);
            totalBehinds--;
            if (totalBehinds < 10)
            {
                String behinds = "  " + totalBehinds.ToString();
                behindTotal2.Content = behinds;
            }
            else if (totalBehinds >= 10 && totalBehinds < 100)
            {
                String behinds = " " + totalBehinds.ToString();
                behindTotal2.Content = behinds;
            }
            else
            {
                behindTotal2.Content = totalBehinds.ToString();
            }

            //Convert total score to integer, minus 1 point and update label
            int totalScore = Convert.ToInt32(total2.Content);
            totalScore--;
            if (totalScore < 10)
            {
                String total = "  " + totalScore.ToString();
                total2.Content = total;
            }
            else if (totalScore >= 10 && totalScore < 100)
            {
                String total = " " + totalScore.ToString();
                total2.Content = total;
            }
            else
            {
                total2.Content = totalScore.ToString();
            }

            writeToTextFile();
        }

        private void _1stQ_Click(object sender, RoutedEventArgs e)
        {
            //update qtr on app and in txt file
            qtrDisplay.Content = "1st";
            File.WriteAllText(@"C:\\demos\\footyTimer.txt", qtrDisplay.Content + "\n" + TimeDisplay.Content.ToString());
        }

        private void _2ndQ_Click(object sender, RoutedEventArgs e)
        {
            //update qtr on app and in txt file
            qtrDisplay.Content = "2nd";
            File.WriteAllText(@"C:\\demos\\footyTimer.txt", qtrDisplay.Content + "\n" + TimeDisplay.Content.ToString());
        }

        private void _3rd_Click(object sender, RoutedEventArgs e)
        {
            //update qtr on app and in txt file
            qtrDisplay.Content = "3rd";
            File.WriteAllText(@"C:\\demos\\footyTimer.txt", qtrDisplay.Content + "\n" + TimeDisplay.Content.ToString());
        }

        private void _4th_Click(object sender, RoutedEventArgs e)
        {
            //update qtr on app and in txt file
            qtrDisplay.Content = "4th";
            File.WriteAllText(@"C:\\demos\\footyTimer.txt", qtrDisplay.Content + "\n" + TimeDisplay.Content.ToString());
        }

    }
}
