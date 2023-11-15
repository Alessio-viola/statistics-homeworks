using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SecurityScoreSimulation
{
    public partial class MainForm : Form
    {
        private readonly Random random = new Random();

        public MainForm()
        {
            InitializeComponent();
        }

        private void StartSimulationButton_Click(object sender, EventArgs e)
        {
            int numSystems = (int)numSystemsInput.Value;
            int numAttacks = (int)numAttacksInput.Value;
            double successProbability = (double)successProbabilityInput.Value;
            int securityScore = (int)securityScoreInput.Value;
            int penetrationScore = (int)penetrationScoreInput.Value;

            List<List<int>> scores = SimulateSecurityScores(numSystems, numAttacks, successProbability, securityScore, penetrationScore);

            DrawSecurityScores(scores, myCanvas);
            DrawHorizontalHistogram(GetMatrixLastColumn(scores), histogramCanvas, numAttacks);

            int selectedDay = (int)dayInput.Value;
            DrawHorizontalHistogram(GetChooseColumn(scores, selectedDay), histogram1DaySpecified, selectedDay);
        }

        private List<List<int>> SimulateSecurityScores(int numSystems, int numAttacks, double successProbability, int securityScore, int penetrationScore)
        {
            List<List<int>> scores = new List<List<int>>();

            for (int i = 0; i < numSystems; i++)
            {
                List<int> systemScores = new List<int>();
                int scorePenetratedAttacks = 0;
                int scoreDefendedAttacks = 0;

                for (int attack = 1; attack <= numAttacks; attack++)
                {
                    if (random.NextDouble() < successProbability)
                    {
                        scorePenetratedAttacks++; // System is penetrated
                    }
                    else
                    {
                        scoreDefendedAttacks++;
                    }

                    systemScores.Add(scorePenetratedAttacks);

                    // Check if the system should be discarded if the system reach P before S
                    if (scorePenetratedAttacks >= penetrationScore && scoreDefendedAttacks < securityScore)
                    {
                        systemScores.Clear();
                        break; // the system is discarded and will not be present in the chart
                    }
                }

                scores.Add(systemScores);
            }

            return scores;
        }

        private void DrawSecurityScores(List<List<int>> scores, PictureBox canvas)
        {
            Graphics g = canvas.CreateGraphics();
            g.Clear(Color.White);

            int numTrajectories = scores.Count;
            int width = canvas.Width;
            int height = canvas.Height;

            // Scale the scores for drawing
            int minScore = 0;
            int maxScore = scores.Max(systemScores => systemScores.Max());

            int scoreRange = maxScore - minScore;
            float scoreScale = (float)height / scoreRange;

            // Rest of your drawing logic goes here
            for (int i = 0; i < numTrajectories; i++)
            {
                using (Pen pen = new Pen(GetRandomColor(), 1))
                {
                    List<int> systemScores = scores[i];
                    for (int j = 0; j < numAttacks; j++)
                    {
                        float x = j * (width - 50) / numAttacks + 50;
                        float y = height - (systemScores[j] - minScore) * scoreScale;
                        if (j == 0)
                        {
                            g.MoveTo(x, y);
                        }
                        else
                        {
                            g.LineTo(x, y);
                        }
                    }
                    g.DrawPath(pen, g.GetPath());
                }
            }

            g.Dispose();
        }

        private void DrawHorizontalHistogram(List<int> data, PictureBox canvas, int numAttacks)
        {
            Graphics g = canvas.CreateGraphics();
            g.Clear(Color.White);

            int maxCount = data.Max();
            int minCount = data.Min();
            int numIntervals = maxCount - minCount;
            int barHeight = canvas.Height / (numAttacks * 2);

            // Rest of your histogram drawing logic goes here
            for (int i = 0; i < numAttacks; i++)
            {
                float barWidth = (float)data.Count(d => d == i) / data.Count * canvas.Width;
                float y = i * barHeight;

                using (Brush brush = new SolidBrush(Color.Blue))
                {
                    g.FillRectangle(brush, 0, y, barWidth, barHeight);
                }

                using (Font font = new Font("Arial", 8))
                using (Brush textBrush = new SolidBrush(Color.Black))
                {
                    g.DrawString($"[{i};{i + 1})", font, textBrush, barWidth + 5, y + barHeight / 2);
                }
            }

            g.Dispose();
        }

        private List<int> GetMatrixLastColumn(List<List<int>> matrix)
        {
            return matrix.Select(row => row.Last()).ToList();
        }

        private List<int> GetChooseColumn(List<List<int>> matrix, int column)
        {
            return matrix.Select(row => row.ElementAt(column - 1)).ToList();
        }

        private Color GetRandomColor()
        {
            return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }
    }
}
