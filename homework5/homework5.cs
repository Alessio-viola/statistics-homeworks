using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class SecurityScoreSimulation : Form
{
    private Label labelNumSystems;
    private Label labelNumAttacks;
    private Label labelSuccessProbability;
    private Label labelTime;
    private Label labelDay;
    private Button startSimulationButton;
    private ChartCanvas chartCanvas;
    private HistogramCanvas histogramCanvas;
    private HistogramCanvas histogram1DaySpecified;

    public SecurityScoreSimulation()
    {
        this.Text = "Security Score Simulation";
        this.Size = new Size(800, 600);

        labelNumSystems = new Label();
        labelNumSystems.Text = "Number of Systems (M):";
        labelNumSystems.Location = new Point(10, 10);
        this.Controls.Add(labelNumSystems);

        TextBox numSystemsInput = new TextBox();
        numSystemsInput.Location = new Point(200, 10);
        numSystemsInput.Text = "10";
        this.Controls.Add(numSystemsInput);

        labelNumAttacks = new Label();
        labelNumAttacks.Text = "Number of SubIntervals (N):";
        labelNumAttacks.Location = new Point(10, 40);
        this.Controls.Add(labelNumAttacks);

        TextBox numAttacksInput = new TextBox();
        numAttacksInput.Location = new Point(200, 40);
        numAttacksInput.Text = "20";
        this.Controls.Add(numAttacksInput);

        labelSuccessProbability = new Label();
        labelSuccessProbability.Text = "Lambda:";
        labelSuccessProbability.Location = new Point(10, 70);
        this.Controls.Add(labelSuccessProbability);

        TextBox successProbabilityInput = new TextBox();
        successProbabilityInput.Location = new Point(200, 70);
        successProbabilityInput.Text = "50";
        this.Controls.Add(successProbabilityInput);

        labelTime = new Label();
        labelTime.Text = "Time (T):";
        labelTime.Location = new Point(10, 100);
        this.Controls.Add(labelTime);

        TextBox timeInput = new TextBox();
        timeInput.Location = new Point(200, 100);
        timeInput.Text = "1";
        this.Controls.Add(timeInput);

        labelDay = new Label();
        labelDay.Text = "Choose a day:";
        labelDay.Location = new Point(10, 130);
        this.Controls.Add(labelDay);

        TextBox dayInput = new TextBox();
        dayInput.Location = new Point(200, 130);
        dayInput.Text = "10";
        this.Controls.Add(dayInput);

        startSimulationButton = new Button();
        startSimulationButton.Text = "Start Simulation";
        startSimulationButton.Location = new Point(10, 160);
        startSimulationButton.Click += StartSimulationButton_Click;
        this.Controls.Add(startSimulationButton);

        chartCanvas = new ChartCanvas();
        chartCanvas.Location = new Point(10, 200);
        this.Controls.Add(chartCanvas);

        histogramCanvas = new HistogramCanvas();
        histogramCanvas.Location = new Point(270, 200);
        this.Controls.Add(histogramCanvas);

        histogram1DaySpecified = new HistogramCanvas();
        histogram1DaySpecified.Location = new Point(530, 200);
        this.Controls.Add(histogram1DaySpecified);
    }

    private void StartSimulationButton_Click(object sender, EventArgs e)
    {
        int numSystems = int.Parse(numSystemsInput.Text);
        int numAttacks = int.Parse(numAttacksInput.Text);
        int T = int.Parse(timeInput.Text);
        double lambda = double.Parse(successProbabilityInput.Text);

        double successProbabilityInput = lambda * T / numAttacks;

        int day = int.Parse(dayInput.Text);

        List<int[]> scores = new List<int[]>();

        Random rand = new Random();

        for (int i = 0; i < numSystems; i++)
        {
            int[] systemScores = new int[numAttacks];
            int score = 0;

            for (int attack = 1; attack <= numAttacks; attack++)
            {
                if (rand.NextDouble() < successProbabilityInput)
                {
                    score += 1;
                }

                systemScores[attack - 1] = score;
            }

            scores.Add(systemScores);
        }

        chartCanvas.DrawSecurityScores(scores);
        histogramCanvas.DrawHorizontalHistogram(GetMatrixLastColumn(scores), numAttacks);
        histogram1DaySpecified.DrawHorizontalHistogram(GetChooseColumn(scores, day), day);
    }

    private int[] GetMatrixLastColumn(List<int[]> matrix)
    {
        int numRows = matrix.Count;
        int[] lastColumn = new int[numRows];

        for (int i = 0; i < numRows; i++)
        {
            lastColumn[i] = matrix[i][matrix[i].Length - 1];
        }

        return lastColumn;
    }

    private int[] GetChooseColumn(List<int[]> matrix, int column)
    {
        int numRows = matrix.Count;
        int[] chooseColumn = new int[numRows];

        for (int i = 0; i < numRows; i++)
        {
            chooseColumn[i] = matrix[i][column - 1];
        }

        return chooseColumn;
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new SecurityScoreSimulation());
    }
}

public class ChartCanvas : Panel
{
    private List<int[]> scores;

    public ChartCanvas()
    {
        scores = new List<int[]>();
    }

    public void DrawSecurityScores(List<int[]> scores)
    {
        this.scores = scores;
        this.Refresh(); // Redraw the panel
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (scores.Count == 0)
            return;

        Graphics g = e.Graphics;
        int width = this.Width;
        int height = this.Height;

        // Find the maximum and minimum scores for scaling
        int minScore = int.MaxValue;
        int maxScore = int.MinValue;

        foreach (int[] systemScores in scores)
        {
            int systemMin = systemScores.Min();
            int systemMax = systemScores.Max();

            if (systemMin < minScore)
                minScore = systemMin;

            if (systemMax > maxScore)
                maxScore = systemMax;
        }

        int scoreRange = maxScore - minScore;
        float scoreScale = (float)height / scoreRange;

        // Draw the y-axis (ordinates)
        g.DrawLine(Pens.Black, 50, 0, 50, height);

        // Draw the x-axis (abscissas)
        g.DrawLine(Pens.Black, 50, height, width, height);

        // Draw labels for the y-axis
        for (int i = minScore; i <= maxScore; i++)
        {
            float labelY = height - (i - minScore) * scoreScale;
            g.DrawString(i.ToString(), new Font("Arial", 8), Brushes.Black, 30, labelY);
        }

        // Draw numbers for the x-axis (attacks)
        for (int i = 0; i <= numAttacks; i += 10)
        {
            float labelX = (i / numAttacks) * (width - 50) + 50;
            g.DrawString(i.ToString(), new Font("Arial", 8), Brushes.Black, labelX, height + 20);
        }

        // Draw score trajectories for all systems
        Random rand = new Random();

        foreach (int[] systemScores in scores)
        {
            Color randomColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
            Pen pen = new Pen(randomColor, 2);

            for (int i = 0; i < numAttacks; i++)
            {
                float x = (i / (float)numAttacks) * (width - 50) + 50;
                float y = height - (systemScores[i] - minScore) * scoreScale;

                if (i == 0)
                    g.DrawLine(pen, x, y, x, y);
                else
                    g.DrawLine(pen, x - 1, y - 1, x, y);

                pen.Dispose();
            }
        }
    }
}

public class HistogramCanvas : Panel
{
    private int[] data;
    private int numAttacks;

    public HistogramCanvas()
    {
        data = new int[0];
        numAttacks = 0;
    }

    public void DrawHorizontalHistogram(int[] data, int numAttacks)
    {
        this.data = data;
        this.numAttacks = numAttacks;
        this.Refresh(); // Redraw the panel
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (data.Length == 0 || numAttacks == 0)
            return;

        Graphics g = e.Graphics;
        int width = this.Width;
        int height = this.Height;

        int maxCount = data.Max();
        int minCount = data.Min();
        int numIntervals = maxCount - minCount;
        int barHeight = height / (numAttacks * 2);

        for (int i = 0; i < numAttacks; i++)
        {
            int barWidth = (data[i] / numSystems) * width;
            int y = i * barHeight;

            Color randomColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
            Brush brush = new SolidBrush(randomColor);

            g.FillRectangle(brush, 0, y, barWidth, barHeight);
            g.DrawString($"[{i};{i + 1})", new Font("Arial", 8), Brushes.Black, barWidth + 5, y + barHeight / 2);
        }
    }
}
