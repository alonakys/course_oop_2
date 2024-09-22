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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace course_oop_2
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        static Random r = new Random();
        private int score = 0;
        private double spawnRate = 600; //GAMEPLAY PARAMETER!//Spawns an item every X milliseconds
        private double deltaSpawnRate = 0; //counts the milliseconds 
        private double ticksToFullSecond = 0; //gonna summ up some second ticks, into the full second, and act on it when it's full
        private double MousePosX = 0;
        private string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "users.json");

        private TextBlock timerTextBlock;
        private TextBlock highScoreTextBlock;
        private DispatcherTimer gameTimer;
        private TimeSpan timeLeft;
        private Game game;
        private List<Thing> things;

        private bool gameEnded;
        public GameWindow()
        {
            things = new List<Thing>();
            InitializeComponent();
            InitializeUIElements();
            SetupGameTimer();
            DisplayUIElementsOn(myCanvas);
            game = new Game();

            gameplace.Fill = game.backgroundImage;

            player1.Fill = game.basket.Imagee;
            StartGameTimer();
        }
        public class Game
        {
            public Basket basket;
            public ImageBrush backgroundImage;
            public Game()
            {
                basket = new Basket();

                backgroundImage = new ImageBrush(); // new background image brush - will be used to show background
                backgroundImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pngeggBackground.jpg"));

            }

            public void Start()
            {

            }
        }
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            // Get the x and y coordinates of the mouse pointer.
            System.Windows.Point position = e.GetPosition(this);
            MousePosX = position.X;//Saved, for the later use on item collection
            Canvas.SetLeft(player1, MousePosX - 200);
        }
        private void InitializeUIElements()
        {
            timerTextBlock = new TextBlock
            {
                FontSize = 30,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Black),
                HorizontalAlignment = HorizontalAlignment.Right
            };

            highScoreTextBlock = new TextBlock
            {
                Text = $"Score: {score}",
                FontSize = 24,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Black),
                HorizontalAlignment = HorizontalAlignment.Right
            };
        }

        public void StartGameTimer()
        {
            if (!gameTimer.IsEnabled)
            {
                gameTimer.Start();
            }
        }
        private void DisplayUIElementsOn(Canvas parentCanvas)
        {
            if (!parentCanvas.Children.Contains(timerTextBlock))
                parentCanvas.Children.Add(timerTextBlock);


            if (!parentCanvas.Children.Contains(highScoreTextBlock))
                parentCanvas.Children.Add(highScoreTextBlock);


            UpdateUIElementsPosition(parentCanvas);
        }
        private void UpdateUIElementsPosition(Canvas parentCanvas)
        {
            double topMargin = 5;
            double rightPosition = parentCanvas.ActualWidth - timerTextBlock.ActualWidth;

            Canvas.SetTop(timerTextBlock, topMargin);
            Canvas.SetLeft(timerTextBlock, rightPosition);

            topMargin = 50;
            rightPosition = parentCanvas.ActualWidth - highScoreTextBlock.ActualWidth;

            Canvas.SetTop(highScoreTextBlock, topMargin);
            Canvas.SetLeft(highScoreTextBlock, rightPosition);

        }
        private void SetupGameTimer()
        {
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromSeconds(0.016);
            gameTimer.Tick += GameTimer_Tick;
            timeLeft = TimeSpan.FromSeconds(20);
        }

        private void TimeSkip(object sender, EventArgs e) { }

        private void ManageTimeVariables()
        {
            deltaSpawnRate += gameTimer.Interval.TotalMilliseconds;
            ticksToFullSecond += gameTimer.Interval.TotalMilliseconds;

            if (deltaSpawnRate >= spawnRate) // Spawns items every spawnRate ms
            {
                deltaSpawnRate -= spawnRate;
                MakeThings();
            }
            if (ticksToFullSecond >= 1000) //One second passed
            {
                ticksToFullSecond -= 1000;
                if (timeLeft.TotalSeconds >= 1)
                {
                    timeLeft = timeLeft.Add(TimeSpan.FromSeconds(-1));
                    timerTextBlock.Text = $"Time: {timeLeft.TotalSeconds}";
                    if (timeLeft.TotalSeconds <= 1)// Is the game over?
                        ticksToFullSecond = 1000;
                }
                else if (!gameEnded)
                {
                    gameEnded = true;
                    gameTimer.Stop();

                    User newuser = new User(UserNameWindow.userName, score);
                    ResaultsWindow res = new ResaultsWindow(newuser);

                    Close();
                    res.ShowDialog();


                }
            }
        }
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            gameTimer.Tick += TimeSkip; // run the make presents function
            ManageTimeVariables();
            ThingsFall();
        }

        private void ThingsFall()
        {
            if (things != null && things.Count > 0)
                for (int i = 0; i < things.Count; i++)
                {
                    things[i].Move();
                    UpdateMoveInCanvas(things[i]);

                    double diffX = Math.Abs(things[i].Position.X - MousePosX);//distance between a trolley and an item
                    if (things[i].Position.Y > 420 && things[i].Position.Y < 465 && diffX < 150)
                    {
                        things[i].Benefit(ref score, things[i].scoreValue, things[i].timeValue, ref timeLeft);
                        highScoreTextBlock.Text = $"Score: {score}";

                        timerTextBlock.Text = $"Time: {timeLeft.TotalSeconds}";

                        RemoveFromCanvas(things[i]);
                        things.RemoveAt(i);
                    }
                    else if (things[i].Position.Y > 800)
                    {
                        RemoveFromCanvas(things[i]);
                        things.RemoveAt(i);
                    }
                }
        }

        private void UpdateMoveInCanvas(Thing t)
        {
            Canvas.SetLeft(t.myRectangle, t.Position.X);
            Canvas.SetTop(t.myRectangle, t.Position.Y);
        }
        private void RemoveFromCanvas(Thing t)
        {
            int id = myCanvas.Children.IndexOf(t.myRectangle);
            myCanvas.Children.RemoveAt(id);
        }
        private void MakeThings()
        {
            Thing thing = new RedFood();
            Objects objectType = (Objects)r.Next(8);
            thing = CreateThing(objectType);
            things.Add(thing);

            Rectangle newRec = new Rectangle
            {
                Tag = "drops",
                Width = 60,
                Height = 60,
                Fill = thing.Imagee,
                Opacity = 1,
            };
            thing.myRectangle = newRec;
            Canvas.SetLeft(thing.myRectangle, r.Next(10, 1070));
            Canvas.SetTop(thing.myRectangle, -200);
            // once the location is set now we can add it to the canvas
            myCanvas.Children.Add(thing.myRectangle);
        }

        private Thing CreateThing(Objects objectType)
        {
            //жалке подобіє поліморфізма
            switch (objectType)
            {
                case Objects.Apple:
                    return new RedFood();
                case Objects.Tomato:
                    return new RedFood();
                case Objects.Cucumber:
                    return new GreenFood();
                case Objects.Banana:
                    return new YellowFood();
                case Objects.Orange:
                    return new YellowFood();
                case Objects.Ball:
                    return new Toy();
                case Objects.Bear:
                    return new Toy();
                case Objects.Car:
                    return new Toy();
                default:
                    return new RedFood();//just in case
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timeLeft = timeLeft.Add(TimeSpan.FromSeconds(-timeLeft.TotalSeconds));//Button's Clicked == Time's up
            ticksToFullSecond = 1000;//milliseconds too!
        }
    }
}
