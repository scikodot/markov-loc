using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MarkovLS
{
    public struct Robot
    {
        public Label tile;  //the tile on which the robot is located
        public int X, Y;  //coordinates (array indexes) of the robot on tilemap
        public Color prev_color;  //the previous color of the tile on which the robot is located
    }

    public partial class Form1 : Form
    {
        public bool map_set = false;  //whether map is already set
        public Point LeftTop = new Point(10, 10), map_center;  //left-top coordinate of the map; map central tile (either precisely or approximately)
        public int side = 50;  //side length of the tile
        public int map_width, map_height;  //width/height of the current map
        public int num = 0;  //numeric value, used to set IDs to all the tiles
        public Label[,] tiles;  //tiles of the map

        public bool robot_to_place = false, robot_placed = false;  //does robot need to be placed?; is robot placed?
        public Robot robot;  //struct, describing all comprehensive data about currently placed robot
        public Color measurement;  //current sensor's measurement
        public double pHit, pMiss, pExact, pUndershoot, pOvershoot;  //sensing and moving (default) probabilities
        public int distance;  //sensor's measuring distance by X and Y axis (tiles in between are calculated from this value as well)
        public string[] buffer = new string[buffer_size + 1];  //buffer, used to increase precision of moving measurements
        static public int buffer_size = 5;  //amount of tiles written to buffer
        public bool sensing_set = false, moving_set = false, distance_set = false;  //these tell whether there are probabilities for each model and distance

        public Random rng = new Random();
        public Pen pen;
        public Graphics graph;

        public Form1()  //initialize form
        {
            InitializeComponent();
            this.Text = "Markov localization simulator";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            foreach (Control control in this.Controls)
            {
                control.PreviewKeyDown += new PreviewKeyDownEventHandler(arrows_PreviewKeyDown);
            }

            this.KeyPreview = true;
            this.PreviewKeyDown += new PreviewKeyDownEventHandler(arrows_PreviewKeyDown);
            this.KeyDown += new KeyEventHandler(arrows_KeyDown);
        }

        private void generate_Click(object sender, EventArgs e)  //generating map
        {
            int width_to_set = 0, height_to_set = 0;
            if (width_set.Text != "")
                width_to_set = int.Parse(width_set.Text);
            if (height_set.Text != "")
                height_to_set = int.Parse(height_set.Text);

            if (width_to_set * height_to_set >= 4 && width_to_set <= 15 && height_to_set <= 15)
            {
                map_width = width_to_set;
                map_height = height_to_set;

                map_center = new Point(map_width % 2 == 1 ? (map_width - 1) / 2 : rng.Next(map_width / 2 - 1, map_width / 2 + 1),
                    map_height % 2 == 1 ? (map_height - 1) / 2 : rng.Next(map_height / 2 - 1, map_height / 2 - 1));

                color.Text = "none";
                color.ForeColor = Color.Black;
                if (tiles != null)
                    EraseTiles();
                Refresh();

                tiles = new Label[map_height, map_width];
                AddTiles();
                DrawMap();
                map_set = true;
            }
            else
            {
                MessageBox.Show("You've entered wrong map dimensions.\nThere must be at least 4 tiles in total and not more\n" +
                    "than 15 tiles by each dimension.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddTiles()  //adding tiles to the map array
        {
            num = 0;
            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    tiles[i, j] = new Label
                    {
                        Name = "label_" + num++.ToString(),
                        Location = new Point(LeftTop.X + j * side + j + 1, LeftTop.Y + i * side + i + 1),
                        AutoSize = false,
                        Size = new Size(side, side),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Text = "0,0000",
                        BackColor = GenColor(),
                    };
                    tiles[i, j].MouseClick += new MouseEventHandler(tile_Click);
                }
        }

        private Color GenColor()  //randomly generate color for the tile
        {
            int rand = rng.Next(0, 2);
            switch (rand)
            {
                case 0:
                    return Color.Green;
                case 1:
                    return Color.Red;
                default:
                    return SystemColors.Control;
            }
        }

        private void DrawMap()  //drawing map
        {
            DrawBounds();
            DrawTiles();
        }

        private void DrawBounds()  //drawing bounds and inside lines of the map
        {
            pen = new Pen(Color.Black);
            graph = this.CreateGraphics();

            for (int i = 0; i <= tiles.GetLength(1); i++)
            {
                graph.DrawLine(pen,
                    new Point(LeftTop.X + i * side + i, LeftTop.Y),
                    new Point(LeftTop.X + i * side + i, LeftTop.Y + map_height * side + map_height));
            }

            for (int i = 0; i <= tiles.GetLength(0); i++)
            {
                graph.DrawLine(pen,
                    new Point(LeftTop.X, LeftTop.Y + i * side + i),
                    new Point(LeftTop.X + map_width * side + map_width, LeftTop.Y + i * side + i));
            }

            pen.Dispose();
            graph.Dispose();
        }

        private void DrawTiles()  //drawing tiles
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                    Controls.Add(tiles[i, j]);
        }

        private void clear_Click(object sender, EventArgs e)  //clear map
        {
            color.Text = "none";
            color.ForeColor = Color.Black;
            EraseTiles();
            Refresh();
            map_set = false;
        }

        private void EraseTiles()  //deleting tiles
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                    Controls.Remove(tiles[i, j]);
        }

        private void SenseProbs_set_Click(object sender, EventArgs e)  //setting sensing probabilities
        {
            double pHit_to_set = 0, pMiss_to_set = 0;
            if (pHit_set.Text != "")
            {
                string s = pHit_set.Text.Contains(".") ? pHit_set.Text.Replace(".", ",") : pHit_set.Text;
                pHit_to_set = double.Parse(s);
            }
            if (pMiss_set.Text != "")
            {
                string s = pMiss_set.Text.Contains(".") ? pMiss_set.Text.Replace(".", ",") : pMiss_set.Text;
                pMiss_to_set = double.Parse(s);
            }

            if (pHit_to_set >= 0 && pMiss_to_set >= 0 && pHit_to_set + pMiss_to_set == 1)
            {
                pHit = pHit_to_set;
                pHit_label.Text = "pHit: " + pHit.ToString();
                pMiss = pMiss_to_set;
                pMiss_label.Text = "pMiss: " + pMiss.ToString();
                sensing_set = true;
            }
            else
            {
                MessageBox.Show("You've entered wrong sensing probabilities.\nThe total probability must be equal to 1 and each of them\n" +
                    "must be a positive value.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MoveProbs_set_Click(object sender, EventArgs e)  //setting moving probabilities
        {
            double pExact_to_set = 0, pUndershoot_to_set = 0, pOvershoot_to_set = 0;
            if (pExact_set.Text != "")
            {
                string s = pExact_set.Text.Contains(".") ? pExact_set.Text.Replace(".", ",") : pExact_set.Text;
                pExact_to_set = double.Parse(s);
            }
            if (pUndershoot_set.Text != "")
            {
                string s = pUndershoot_set.Text.Contains(".") ? pUndershoot_set.Text.Replace(".", ",") : pUndershoot_set.Text;
                pUndershoot_to_set = double.Parse(s);
            }
            if (pOvershoot_set.Text != "")
            {
                string s = pOvershoot_set.Text.Contains(".") ? pOvershoot_set.Text.Replace(".", ",") : pOvershoot_set.Text;
                pOvershoot_to_set = double.Parse(s);
            }

            if (pExact_to_set >= 0 && pUndershoot_to_set >= 0 && pOvershoot_to_set >= 0 && pExact_to_set + pUndershoot_to_set + pOvershoot_to_set == 1)
            {
                pExact = pExact_to_set;
                pExact_label.Text = "pExact: " + pExact.ToString();
                pUndershoot = pUndershoot_to_set;
                pUndershoot_label.Text = "pUndershoot: " + pUndershoot.ToString();
                pOvershoot = pOvershoot_to_set;
                pOvershoot_label.Text = "pOvershoot: " + pOvershoot.ToString();
                moving_set = true;
            }
            else
            {
                MessageBox.Show("You've entered wrong moving probabilities.\nThe total probability must be equal to 1 and each of them\n" +
                    "must be a positive value.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dist_set_Click(object sender, EventArgs e)  //setting presence distance
        {
            int dist_to_set;
            if (int.TryParse(dist.Text, out dist_to_set))
            {
                if (dist_to_set > 0)
                {
                    distance = dist_to_set;
                    distance_label.Text = "distance: " + distance.ToString();
                    distance_set = true;
                }
                else
                    MessageBox.Show("You've entered wrong distance.\nThe distance must be a positive integer value.\n",
                        "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("You've entered wrong distance.\nThe distance must be a positive integer value.\n",
                        "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void fill_default_Click(object sender, EventArgs e)  //filling default values
        {
            pHit = 0.8; pHit_label.Text = "pHit: " + pHit.ToString();
            pMiss = 0.2; pMiss_label.Text = "pMiss: " + pMiss.ToString();
            pExact = 0.8; pExact_label.Text = "pExact: " + pExact.ToString();
            pUndershoot = 0.1; pUndershoot_label.Text = "pUndershoot: " + pUndershoot.ToString();
            pOvershoot = 0.1; pOvershoot_label.Text = "pOvershoot: " + pOvershoot.ToString();
            distance = 3; distance_label.Text = "distance: " + distance.ToString();
            sensing_set = moving_set = distance_set = true;
        }

        private void place_robot_Click(object sender, EventArgs e)  //telling whether we may place the robot or not
        {
            if (map_set && sensing_set && moving_set && distance_set)  //if the map is created and all the probabilities are set, we may continue
                robot_to_place = true;
            else
            {
                if (!map_set)
                    MessageBox.Show("You've not created the map.\nPlease, create it so there is a place to put the robot on.",
                        "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (!sensing_set || !moving_set)
                    MessageBox.Show("You've not entered sensing and/or moving probabilities.\nPlease, enter them to place the robot.",
                        "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (!distance_set)
                    MessageBox.Show("You've not entered sensing distance.\nPlease, enter it to place the robot.",
                        "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tile_Click(object sender, MouseEventArgs e)  //placing robot on a tile
        {
            Label tile = (Label)sender;
            if (robot_to_place)
            {
                if (robot_placed)
                {
                    robot.tile.BackColor = robot.prev_color;
                    ClearTiles();
                }
                robot = new Robot
                {
                    tile = tile,
                    prev_color = tile.BackColor,
                    X = (tile.Location.X - LeftTop.X - 1) / (side + 1),
                    Y = (tile.Location.Y - LeftTop.Y - 1) / (side + 1)
                };
                robot.tile.BackColor = Color.Yellow;
                robot.tile.Text = "1,0000";
                color.Text = "none";
                color.ForeColor = Color.Black;
                robot_to_place = false;
                robot_placed = true;
            }
        }

        private void ClearTiles()  //resetting tiles' text
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                    tiles[i, j].Text = "0,0000";
        }

        private void arrows_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)  //making arrow keys recognizable
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    e.IsInputKey = true;
                    break;
            }
        }

        private void arrows_KeyDown(object sender, KeyEventArgs e)  //arrow keys pressing handler
        {
            if (e.KeyCode == Keys.Up && robot_placed)
            {
                if (buffer[0] != "up")  //if the buffer is not initialized yet, initializing it
                {
                    buffer = new string[buffer_size + 1];
                    buffer[0] = "up";
                }
                AddToBuffer(buffer, tiles[tiles.GetLength(0) - 1, robot.X].Text);
                if (robot.Y > map_center.Y)
                {
                    robot.tile.BackColor = robot.prev_color;
                    robot = new Robot
                    {
                        tile = tiles[robot.Y - 1, robot.X],
                        prev_color = tiles[robot.Y - 1, robot.X].BackColor,
                        X = (tiles[robot.Y - 1, robot.X].Location.X - LeftTop.X - 1) / (side + 1),
                        Y = (tiles[robot.Y - 1, robot.X].Location.Y - LeftTop.Y - 1) / (side + 1)
                    };
                    robot.tile.BackColor = Color.Yellow;

                    move(e.KeyCode);
                    sense();
                }
                else
                    RedrawMap(e.KeyCode);
            }
            else if (e.KeyCode == Keys.Down && robot_placed)
            {
                if (buffer[0] != "down")
                {
                    buffer = new string[buffer_size + 1];
                    buffer[0] = "down";
                }
                AddToBuffer(buffer, tiles[0, robot.X].Text);
                if (robot.Y < map_center.Y)
                {
                    robot.tile.BackColor = robot.prev_color;
                    robot = new Robot
                    {
                        tile = tiles[robot.Y + 1, robot.X],
                        prev_color = tiles[robot.Y + 1, robot.X].BackColor,
                        X = (tiles[robot.Y + 1, robot.X].Location.X - LeftTop.X - 1) / (side + 1),
                        Y = (tiles[robot.Y + 1, robot.X].Location.Y - LeftTop.Y - 1) / (side + 1)
                    };
                    robot.tile.BackColor = Color.Yellow;

                    move(e.KeyCode);
                    sense();
                }
                else
                    RedrawMap(e.KeyCode);
            }
            else if (e.KeyCode == Keys.Left && robot_placed)
            {
                if (buffer[0] != "left")
                {
                    buffer = new string[buffer_size + 1];
                    buffer[0] = "left";
                }
                AddToBuffer(buffer, tiles[robot.Y, tiles.GetLength(1) - 1].Text);
                if (robot.X > map_center.X)
                {
                    robot.tile.BackColor = robot.prev_color;
                    robot = new Robot
                    {
                        tile = tiles[robot.Y, robot.X - 1],
                        prev_color = tiles[robot.Y, robot.X - 1].BackColor,
                        X = (tiles[robot.Y, robot.X - 1].Location.X - LeftTop.X - 1) / (side + 1),
                        Y = (tiles[robot.Y, robot.X - 1].Location.Y - LeftTop.Y - 1) / (side + 1)
                    };
                    robot.tile.BackColor = Color.Yellow;

                    move(e.KeyCode);
                    sense();
                }
                else
                    RedrawMap(e.KeyCode);
            }
            else if (e.KeyCode == Keys.Right && robot_placed)
            {
                if (buffer[0] != "right")
                {
                    buffer = new string[buffer_size + 1];
                    buffer[0] = "right";
                }
                AddToBuffer(buffer, tiles[robot.Y, 0].Text);
                if (robot.X < map_center.X)
                {
                    robot.tile.BackColor = robot.prev_color;
                    robot = new Robot
                    {
                        tile = tiles[robot.Y, robot.X + 1],
                        prev_color = tiles[robot.Y, robot.X + 1].BackColor,
                        X = (tiles[robot.Y, robot.X + 1].Location.X - LeftTop.X - 1) / (side + 1),
                        Y = (tiles[robot.Y, robot.X + 1].Location.Y - LeftTop.Y - 1) / (side + 1)
                    };
                    robot.tile.BackColor = Color.Yellow;

                    move(e.KeyCode);
                    sense();
                }
                else
                    RedrawMap(e.KeyCode);
            }
        }

        private void RedrawMap(Keys key)  //redrawing map if the robot is moving further from center
        {
            for (int i = tiles.GetLength(0) - 1; i >= 0; i--)
                for (int j = tiles.GetLength(1) - 1; j >= 0; j--)
                {
                    switch (key)
                    {
                        case Keys.Up:
                            if (i == 0)
                            {
                                tiles[i, j].Text = "0,0000";
                                if (i == robot.Y && j == robot.X)
                                    robot.prev_color = GenColor();
                                else
                                    tiles[i, j].BackColor = GenColor();
                            }
                            else
                            {
                                tiles[i, j].Text = tiles[i - 1, j].Text;
                                if (i == robot.Y && j == robot.X)
                                    robot.prev_color = tiles[i - 1, j].BackColor;
                                else if (i == robot.Y + 1 && j == robot.X)
                                    tiles[i, j].BackColor = robot.prev_color;
                                else
                                    tiles[i, j].BackColor = tiles[i - 1, j].BackColor;
                            }
                            break;
                        case Keys.Down:
                            if (i == 0)
                            {
                                tiles[map_height - 1 - i, j].Text = "0,0000";
                                if (map_height - 1 - i == robot.Y && j == robot.X)
                                    robot.prev_color = GenColor();
                                else
                                    tiles[map_height - 1 - i, j].BackColor = GenColor();
                            }
                            else
                            {
                                tiles[map_height - 1 - i, j].Text = tiles[map_height - i, j].Text;
                                if (map_height - 1 - i == robot.Y && j == robot.X)
                                    robot.prev_color = tiles[map_height - i, j].BackColor;
                                else if (map_height - 1 - i == robot.Y - 1 && j == robot.X)
                                    tiles[map_height - 1 - i, j].BackColor = robot.prev_color;
                                else
                                    tiles[map_height - 1 - i, j].BackColor = tiles[map_height - i, j].BackColor;
                            }
                            break;
                        case Keys.Left:
                            if (j == 0)
                            {
                                tiles[i, j].Text = "0,0000";
                                if (i == robot.Y && j == robot.X)
                                    robot.prev_color = GenColor();
                                else
                                    tiles[i, j].BackColor = GenColor();
                            }
                            else
                            {
                                tiles[i, j].Text = tiles[i, j - 1].Text;
                                if (i == robot.Y && j == robot.X)
                                    robot.prev_color = tiles[i, j - 1].BackColor;
                                else if (i == robot.Y && j == robot.X + 1)
                                    tiles[i, j].BackColor = robot.prev_color;
                                else
                                    tiles[i, j].BackColor = tiles[i, j - 1].BackColor;
                            }
                            break;
                        case Keys.Right:
                            if (j == 0)
                            {
                                tiles[i, map_width - 1 - j].Text = "0,0000";
                                if (i == robot.Y && map_width - 1 - j == robot.X)
                                    robot.prev_color = GenColor();
                                else
                                    tiles[i, map_width - 1 - j].BackColor = GenColor();
                            }
                            else
                            {
                                tiles[i, map_width - 1 - j].Text = tiles[i, map_width - j].Text;
                                if (i == robot.Y && map_width - 1 - j == robot.X)
                                    robot.prev_color = tiles[i, map_width - j].BackColor;
                                else if (i == robot.Y && map_width - 1 - j == robot.X - 1)
                                    tiles[i, map_width - 1 - j].BackColor = robot.prev_color;
                                else
                                    tiles[i, map_width - 1 - j].BackColor = tiles[i, map_width - j].BackColor;
                            }
                            break;
                    }
                }

            move(key);
            sense();
        }

        private void sense()  //sensing
        {
            measurement = GetMeasurement();
            if (measurement == Color.Red)
            {
                color.Text = "Red";
                color.ForeColor = Color.Red;
            }
            else
            {
                color.Text = "Green";
                color.ForeColor = Color.Green;
            }
            List<double> q = new List<double>();
            for (int i = (robot.X - distance > 0 ? robot.X - distance : 0); i <= (robot.X + distance < tiles.GetLength(1) ? robot.X + distance : tiles.GetLength(1) - 1); i++)
            {
                int j_low = robot.Y - (distance - Math.Abs(robot.X - i)), j_high = robot.Y + (distance - Math.Abs(robot.X - i));
                for (int j = j_low > 0 ? j_low : 0; j <= (j_high < tiles.GetLength(0) ? j_high : tiles.GetLength(0) - 1); j++)
                {
                    int hit;
                    if (i == robot.X && j == robot.Y)
                        hit = measurement == robot.prev_color ? 1 : 0;
                    else
                        hit = measurement == tiles[j, i].BackColor ? 1 : 0;
                    double p = double.Parse(tiles[j, i].Text) * (pHit * hit + pMiss * (1 - hit));
                    q.Add(p);
                    tiles[j, i].Text = p.ToString("0.0000");
                }
            }

            double s = q.Sum();

            if (s != 0)
                for (int i = (robot.X - distance > 0 ? robot.X - distance : 0); i <= (robot.X + distance < tiles.GetLength(1) ? robot.X + distance : tiles.GetLength(1) - 1); i++)
                {
                    int j_low = robot.Y - (distance - Math.Abs(robot.X - i)), j_high = robot.Y + (distance - Math.Abs(robot.X - i));
                    for (int j = j_low > 0 ? j_low : 0; j <= (j_high < tiles.GetLength(0) ? j_high : tiles.GetLength(0) - 1); j++)
                    {
                        tiles[j, i].Text = (double.Parse(tiles[j, i].Text) / s).ToString("0.0000");
                    }
                }
        }

        private Color GetMeasurement()  //getting current tile color, depending on the sensing probabilities
        {
            double rand = rng.NextDouble();
            if (rand >= 0 && rand <= pHit)
                return robot.prev_color;
            else
            {
                if (robot.prev_color == Color.Red)
                    return Color.Green;
                else
                    return Color.Red;
            }
        }

        private void move(Keys dir)  //moving
        {
            int length, sign;
            string[] ps;
            string[] buffer_values = GetFromBuffer(buffer);
            if (dir == Keys.Up || dir == Keys.Down)
            {
                if (dir == Keys.Up)
                    sign = 1;
                else
                    sign = -1;

                length = tiles.GetLength(0);
                ps = new string[length];
                for (int i = 0; i < length; i++)
                {
                    double p = double.Parse(tiles[i, robot.X].Text) * pUndershoot;
                    if ((dir == Keys.Up && i < length - 1) || (dir == Keys.Down && i > 0))
                        p += double.Parse(tiles[i + sign, robot.X].Text) * pExact;
                    else if (buffer_values[0] != null)
                        p += double.Parse(buffer_values[0]) * pExact;
                    if ((dir == Keys.Up && i < length - 2) || (dir == Keys.Down && i > 1))
                        p += double.Parse(tiles[i + 2 * sign, robot.X].Text) * pOvershoot;
                    else if (buffer_values[1] != null)
                        p += double.Parse(buffer_values[1]) * pOvershoot;
                    ps[i] = p.ToString("0.0000");
                }
                for (int i = 0; i < length; i++)
                {
                    tiles[i, robot.X].Text = ps[i];
                }

                //applying motion model to area: probability of each tile inside the area has to be recalculated for each movement
                for (int i = (robot.X - distance > 0 ? robot.X - distance : 0); i <= (robot.X + distance < tiles.GetLength(1) ? robot.X + distance : tiles.GetLength(1) - 1); i++)
                {
                    if (i != robot.X)
                    {
                        int j_low = robot.Y + sign - (distance - Math.Abs(robot.X - i)),
                            j_high = robot.Y + sign + (distance - Math.Abs(robot.X - i)),
                            j_low_corr = dir == Keys.Up ? (j_low - 2 > 0 ? j_low - 2 : 0) : (j_low > 0 ? j_low : 0),
                            j_high_corr = dir == Keys.Down ? (j_high + 2 < tiles.GetLength(0) ? j_high + 2 : tiles.GetLength(0) - 1) : (j_high < tiles.GetLength(0) ? j_high : tiles.GetLength(0) - 1);
                        length = j_high_corr - j_low_corr + 1;
                        ps = new string[length];
                        for (int j = j_low_corr; j <= j_high_corr; j++)
                        {
                            double p = 0;
                            if ((dir == Keys.Up && j > j_low) || (dir == Keys.Down && j < j_high))
                                p += double.Parse(tiles[j, i].Text) * pUndershoot;
                            if ((dir == Keys.Up && j < j_high_corr) || (dir == Keys.Down && j > j_low_corr))
                                p += double.Parse(tiles[j + sign, i].Text) * pExact;
                            if ((dir == Keys.Up && j < j_high_corr - 1) || (dir == Keys.Down && j > j_low_corr + 1))
                                p += double.Parse(tiles[j + 2 * sign, i].Text) * pOvershoot;
                            ps[j - j_low_corr] = p.ToString("0.0000");
                        }
                        for (int j = j_low_corr; j <= j_high_corr; j++)
                        {
                            tiles[j, i].Text = ps[j - j_low_corr];
                        }
                    }
                }
            }
            else
            {
                if (dir == Keys.Left)
                    sign = 1;
                else
                    sign = -1;

                length = tiles.GetLength(1);
                ps = new string[length];
                for (int i = 0; i < length; i++)
                {
                    double p = double.Parse(tiles[robot.Y, i].Text) * pUndershoot;
                    if ((dir == Keys.Left && i < length - 1) || (dir == Keys.Right && i > 0))
                        p += double.Parse(tiles[robot.Y, i + sign].Text) * pExact;
                    else if (buffer_values[0] != null)
                        p += double.Parse(buffer_values[0]) * pExact;
                    if ((dir == Keys.Left && i < length - 2) || (dir == Keys.Right && i > 1))
                        p += double.Parse(tiles[robot.Y, i + 2 * sign].Text) * pOvershoot;
                    else if (buffer_values[1] != null)
                        p += double.Parse(buffer_values[1]) * pOvershoot;
                    ps[i] = p.ToString("0.0000");
                }
                for (int i = 0; i < length; i++)
                {
                    tiles[robot.Y, i].Text = ps[i];
                }

                //see case Up/Down
                for (int i = (robot.Y - distance > 0 ? robot.Y - distance : 0); i <= (robot.Y + distance < tiles.GetLength(0) ? robot.Y + distance : tiles.GetLength(0) - 1); i++)
                {
                    if (i != robot.Y)
                    {
                        int j_low = robot.X + sign - (distance - Math.Abs(robot.Y - i)),
                            j_high = robot.X + sign + (distance - Math.Abs(robot.Y - i)),
                            j_low_corr = dir == Keys.Left ? (j_low - 2 > 0 ? j_low - 2 : 0) : (j_low > 0 ? j_low : 0),
                            j_high_corr = dir == Keys.Right ? (j_high + 2 < tiles.GetLength(1) ? j_high + 2 : tiles.GetLength(1) - 1) : (j_high < tiles.GetLength(1) ? j_high : tiles.GetLength(1) - 1);
                        length = j_high_corr - j_low_corr + 1;
                        ps = new string[length];
                        for (int j = j_low_corr; j <= j_high_corr; j++)
                        {
                            double p = 0;
                            if ((dir == Keys.Left && j > j_low) || (dir == Keys.Right && j < j_high))
                                p += double.Parse(tiles[i, j].Text) * pUndershoot;
                            if ((dir == Keys.Left && j < j_high_corr) || (dir == Keys.Right && j > j_low_corr))
                                p += double.Parse(tiles[i, j + sign].Text) * pExact;
                            if ((dir == Keys.Left && j < j_high_corr - 1) || (dir == Keys.Right && j > j_low_corr + 1))
                                p += double.Parse(tiles[i, j + 2 * sign].Text) * pOvershoot;
                            ps[j - j_low_corr] = p.ToString("0.0000");
                        }
                        for (int j = j_low_corr; j <= j_high_corr; j++)
                        {
                            tiles[i, j].Text = ps[j - j_low_corr];
                        }
                    }
                }
            }
        }

        private void AddToBuffer(string[] buffer, string value)  //adding tile to buffer
        {
            if (buffer_size != 0)
            {
                int nonzero = 1;
                for (int i = buffer.Length - 1; i > 0; i--)
                {
                    if (buffer[i] != null)
                    {
                        nonzero = i;
                        break;
                    }
                }
                for (int i = nonzero; i > 0; i--)
                {
                    if (i != 1)
                        buffer[i] = buffer[i - 1];
                    else
                        buffer[i] = value;
                }
            }
        }

        private string[] GetFromBuffer(string[] buffer)  //getting tile from buffer
        {
            if (buffer_size == 2)
                return new string[2] { buffer[1], buffer[2] };
            else if (buffer_size == 1)
                return new string[2] { buffer[1], null };
            else
                return new string[2] { null, null };
        }
    }
}
