﻿using System;
using System.Windows;
using OpenTK.Graphics.OpenGL;
using OpenTK.Wpf;
using GameEngineLibrary;
using GameLibrary;
using GameLibrary.Scenes;
using System.Windows.Controls;
using System.Threading;
using GameLibrary.Scripts.RemoteKeyboardControlScripts;
using System.Collections.Generic;
using GameLibrary.Components;
using System.Windows.Media;
using System.IO;

namespace GameUserInterface
{
    /// <summary>
    /// Логика взаимодействия для главного окна игры.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Сервер
        /// </summary>
        private Server server;

        /// <summary>
        /// Клиент
        /// </summary>
        private Client client;

        /// <summary>
        /// Настройки боевой сцены
        /// </summary>
        private BattleSceneSettings settings;

        /// <summary>
        /// Сцена, которая будет отрисовываться на экране.
        /// </summary>
        private Scene scene;

        /// <summary>
        /// Отрисовщик сцены.
        /// </summary>
        private Renderer renderer;

        private bool isOnlineGameEnded;

        /// <summary>
        /// Объект для передачи состояния клавиатуры по сети
        /// </summary>
        private RemoteKeyboardState remoteKeyboardState = new RemoteKeyboardState();

        private Brush unselected = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        private Brush selected = new SolidColorBrush(Color.FromRgb(191, 55, 160));

        /// <summary>
        /// Создание окна приложения.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            InitStartScreen();
            scene = new BattleScene(this, settings);

            MainMenu.Visibility = Visibility.Visible;
            FirstPanzerInfo.Visibility = Visibility.Hidden;
            SecondPanzerInfo.Visibility = Visibility.Hidden;
            RocketShop.Visibility = Visibility.Hidden;
            WinMenu.Visibility = Visibility.Hidden;

            var WpfControlSettings = new GLWpfControlSettings();
            WpfControlSettings.MajorVersion = 3;
            WpfControlSettings.MinorVersion = 6;
            OpenTKControl.Start(WpfControlSettings);
        }

        private void OpenTKControl_Render(TimeSpan delta)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-Width, Width, Height, -Height, 0d, 1d);

            if (client == null)
            {
                scene.Update(delta);
            }
            renderer.Render();
        }

        private void OpenTKControl_Ready()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);

            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            scene.Init();
            renderer = new Renderer(scene);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (client != null)
            {
                client.DisconnectFromServer();
            }

            client?.Close();
            server?.Close();
            scene.Dispose();
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.Texture2D);
        }

        private void PlayGameBtn_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Hidden;
            RocketShop.Visibility = Visibility.Visible;
        }

        private void QuitGameBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void StartGameBtn_Click(object sender, RoutedEventArgs e)
        {
            RocketShop.Visibility = Visibility.Hidden;
            FirstPanzerInfo.Visibility = Visibility.Visible;
            SecondPanzerInfo.Visibility = Visibility.Visible;

            scene.Dispose();

            settings = new BattleSceneSettings();
            int firstPanzerPowerfulRockets = int.Parse(FirstPanzerPowerfulRockets.Content.ToString());
            int firstPanzerFastRockets = int.Parse(FirstPanzerFastRockets.Content.ToString());
            int firstPanzerRockets = int.Parse(FirstPanzerRockets.Content.ToString());
            int secondPanzerPowerfulRockets = int.Parse(SecondPanzerPowerfulRockets.Content.ToString());
            int secondPanzerFastRockets = int.Parse(SecondPanzerFastRockets.Content.ToString());
            int secondPanzerRockets = int.Parse(SecondPanzerRockets.Content.ToString());
            settings.SetFirstPanzerAmounts(firstPanzerPowerfulRockets, firstPanzerFastRockets, firstPanzerRockets);
            settings.SetSecondPanzerAmounts(secondPanzerPowerfulRockets, secondPanzerFastRockets, secondPanzerRockets);
            settings.FirstPanzerHealth = 100;
            settings.SecondPanzerHealth = 100;
            settings.FirstPanzerControlType = BattleSceneSettings.PanzerControlType.Keyboard;
            settings.SecondPanzerControlType = BattleSceneSettings.PanzerControlType.Keyboard;
            scene = new BattleScene(this, settings);
            scene.Init();
            renderer = new Renderer(scene);
        }

        private void BuyBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Grid grid = button.Parent as Grid;
            Label outputLabel = grid.Children[0] as Label;
            Label moneyLable = (grid.Parent as Grid).Children[0] as Label;

            int amount = int.Parse(button.Content.ToString());
            int value = int.Parse(outputLabel.Content.ToString());
            int money = int.Parse(moneyLable.Content.ToString());
            int cost = int.Parse((grid.Children[1] as Label).Content.ToString()) * amount;

            if (value + amount >= 0 && money - cost >= 0)
            {
                value += amount;
                money -= cost;

                outputLabel.Content = value.ToString();
                moneyLable.Content = money.ToString();
            }
        }

        private void RestartGameBtn_Click(object sender, RoutedEventArgs e)
        {
            WinMenu.Visibility = Visibility.Hidden;
            scene.Dispose();
            scene = new BattleScene(this, settings);
            scene.Init();
            renderer = new Renderer(scene);
        }

        private void MainMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            KeyDown -= MainWindow_KeyDown;
            KeyUp -= MainWindow_KeyUp;

            isOnlineGameEnded = false;
            StartMultiplayerGameBtn.Content = "Start";
            StartMultiplayerGameBtn.IsEnabled = true;
            IsSecondPlayerReadyLabel.Content = "Second Player (NOT CONNECTED)";
            RestartGameBtn.Visibility = Visibility.Visible;
            WinMenu.Visibility = Visibility.Hidden;
            MainMenu.Visibility = Visibility.Visible;
            FirstPanzerCooldown.Visibility = Visibility.Visible;
            SecondPanzerCooldown.Visibility = Visibility.Visible;
            FirstPanzerInfo.Visibility = Visibility.Hidden;
            SecondPanzerInfo.Visibility = Visibility.Hidden;

            if (client != null)
            {
                client.DisconnectFromServer();
            }

            client?.Close();
            client = null;
            server?.Close();
            server = null;
            scene.Dispose();
            InitStartScreen();
            scene = new BattleScene(this, settings);
            scene.Init();
            renderer = new Renderer(scene);
        }

        private void InitStartScreen()
        {
            settings = new BattleSceneSettings();
            settings.SetFirstPanzerAmounts(int.MaxValue, int.MaxValue, int.MaxValue);
            settings.SetSecondPanzerAmounts(int.MaxValue, int.MaxValue, int.MaxValue);
            settings.FirstPanzerHealth = int.MaxValue;
            settings.SecondPanzerHealth = int.MaxValue;
            settings.FirstPanzerControlType = BattleSceneSettings.PanzerControlType.Keyboard;
            settings.SecondPanzerControlType = BattleSceneSettings.PanzerControlType.Keyboard;
        }

        private void MultiplayerBtn_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Hidden;
            ChooseMultiplayerTypeMenu.Visibility = Visibility.Visible;
        }

        private void HostGameBtn_Click(object sender, RoutedEventArgs e)
        {
            ChooseMultiplayerTypeMenu.Visibility = Visibility.Hidden;

            StartServer();
            client = ConnectTo("localhost:4748");

            if (client == null)
            {
                MessageBox.Show("Не получилось создать сервер");
                server.Close();
                ChooseMultiplayerTypeMenu.Visibility = Visibility.Visible;
                return;
            }

            var secondPlayerListenerThread = new Thread(new ThreadStart(ListenSecondPlayer));
            secondPlayerListenerThread.Start();

            StartMultiplayerGameBtn.IsEnabled = false;
            MultiplayerRocketShop.Visibility = Visibility.Visible;
        }

        private void ConnectBtn_Click(object sender, RoutedEventArgs e)
        {
            ChooseMultiplayerTypeMenu.Visibility = Visibility.Hidden;
            ConnectMenu.Visibility = Visibility.Visible;
        }

        private void ConnectToServerBtn_Click(object sender, RoutedEventArgs e)
        {
            ConnectMenu.Visibility = Visibility.Hidden;

            client = ConnectTo(ServerAddressInput.Text);

            if (client == null)
            {
                MessageBox.Show("Не получилось подключиться к серверу");
                ConnectMenu.Visibility = Visibility.Visible;
                return;
            }

            StartMultiplayerGameBtn.Content = "Ready";
            IsSecondPlayerReadyLabel.Content = "Second Player (CONNECTED)";
            MultiplayerRocketShop.Visibility = Visibility.Visible;
        }

        private void StartServer()
        {
            server = new Server();
        }

        private void ListenSecondPlayer()
        {
            var connectedUsers = client.GetConnectedUsersCount();

            while (connectedUsers < 2 && !server.IsClosed())
            {
                Thread.Sleep(100);
                connectedUsers = client.GetConnectedUsersCount();
            }
            if (server.IsClosed()) return;

            Dispatcher.Invoke(() => IsSecondPlayerReadyLabel.Content = "Second Player (CONNECTED)");

            var isSecondPlayerReady = false;

            while (!isSecondPlayerReady && !server.IsClosed())
            {
                Thread.Sleep(100);
                isSecondPlayerReady = client.IsSecondReady();
            }
            if (server.IsClosed()) return;

            Dispatcher.Invoke(() => StartMultiplayerGameBtn.IsEnabled = true);
        }

        private Client ConnectTo(string address)
        {
            var client = new Client(address);
            if (client.ConnectToServer())
            {
                return client;
            }
            else
            {
                client.Close();
                return null;
            }
        }

        private void StartMultiplayerGameBtn_Click(object sender, RoutedEventArgs e)
        {
            StartMultiplayerGameBtn.IsEnabled = false;

            int powerfulRockets = int.Parse(MultiplayerPanzerPowerfulRockets.Content.ToString());
            int fastRockets = int.Parse(MultiplayerPanzerFastRockets.Content.ToString());
            int rockets = int.Parse(MultiplayerPanzerRockets.Content.ToString());

            if (server == null)
            {
                client.SetSecondPanzerAmounts(powerfulRockets, fastRockets, rockets);
            }
            else
            {
                client.SetFirstPanzerAmounts(powerfulRockets, fastRockets, rockets);
            }

            MultiplayerRocketShop.Visibility = Visibility.Hidden;

            var serverListenerThread = new Thread(new ThreadStart(ListenServerUpdates));
            serverListenerThread.Start();

            var scheduledUIUpdateThread = new Thread(new ThreadStart(ScheduledUIUpdate));
            scheduledUIUpdateThread.Start();

            KeyDown += MainWindow_KeyDown;
            KeyUp += MainWindow_KeyUp;

            FirstPanzerInfo.Visibility = Visibility.Visible;
            FirstPanzerCooldown.Visibility = Visibility.Visible;
            SecondPanzerInfo.Visibility = Visibility.Visible;
            SecondPanzerCooldown.Visibility = Visibility.Visible;
        }

        private void ListenServerUpdates()
        {
            while(client != null)
            {
                var gameObjects = client.GetCurrentGameObjects();
                if (gameObjects == null || gameObjects.Count == 0) continue;
                scene.GameObjects = gameObjects;
            }
        }

        private void ScheduledUIUpdate()
        {
            while (client != null && !isOnlineGameEnded)
            {
                UpdatePlayersInfo(scene.GameObjects);
                Thread.Sleep(100);
            }
        }

        private void UpdatePlayersInfo(List<GameObject> gameObjects)
        {
            if (gameObjects.Count < 4) return;
            Dispatcher.Invoke(() => 
            {
                var first = gameObjects[2];
                var firstHealth = first.GetComponent("health") as Health;

                var second = gameObjects[3];
                var secondHealth = second.GetComponent("health") as Health;

                if (firstHealth == null || secondHealth == null)
                {
                    WinMenu.Visibility = Visibility.Visible;
                    RestartGameBtn.Visibility = Visibility.Hidden;
                    var transform = first.GetComponent("transform") as GameEngineLibrary.Transform;
                    WinnerName.Text = "Winner: " + (transform.Position.X < 0 ? "First Player" : "Second Player");
                    isOnlineGameEnded = true;
                    return;
                }

                var firstInventory = first.InnerObjects[0].GetComponent("inventory") as Inventory;

                var isAllEmpty = true;
                FirstPanzerHealth.Value = firstHealth.Value;
                for (int i = 0; i < firstInventory.Amounts.Length; i++)
                {
                    var label = (Label)FirstPanzerInventory.Children[i];
                    label.Content = firstInventory.Amounts[i];
                    label.Foreground = i == firstInventory.Current ? selected : unselected;
                    if (firstInventory.Amounts[i] > 0)
                    {
                        isAllEmpty = false;
                    }
                }

                var secondInventory = second.InnerObjects[0].GetComponent("inventory") as Inventory;

                SecondPanzerHealth.Value = secondHealth.Value;
                for (int i = 0; i < secondInventory.Amounts.Length; i++)
                {
                    var label = (Label)SecondPanzerInventory.Children[i];
                    label.Content = secondInventory.Amounts[i];
                    label.Foreground = i == secondInventory.Current ? selected : unselected;
                }

                if (isAllEmpty)
                {
                    WinMenu.Visibility = Visibility.Visible;
                    RestartGameBtn.Visibility = Visibility.Hidden;
                    WinnerName.Text = "Draw";
                    isOnlineGameEnded = true;
                }
            });
        }

        private void MainWindow_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            UpdateKey(e.Key, false);
        }

        private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            UpdateKey(e.Key, true);
        }

        private void UpdateKey(System.Windows.Input.Key key, bool value)
        {
            switch (key)
            {
                case System.Windows.Input.Key.Q:
                    remoteKeyboardState.KeyQ = value;
                    break;
                case System.Windows.Input.Key.W:
                    remoteKeyboardState.KeyW = value;
                    break;
                case System.Windows.Input.Key.E:
                    remoteKeyboardState.KeyE = value;
                    break;
                case System.Windows.Input.Key.A:
                    remoteKeyboardState.KeyA = value;
                    break;
                case System.Windows.Input.Key.S:
                    remoteKeyboardState.KeyS = value;
                    break;
                case System.Windows.Input.Key.D:
                    remoteKeyboardState.KeyD = value;
                    break;
                case System.Windows.Input.Key.Space:
                    remoteKeyboardState.KeySpace = value;
                    break;
            }

            if (server == null)
            {
                client.SetSecondPlayerKeyboardState(remoteKeyboardState);
								using (var writer = new StreamWriter("stressTest_client.txt", true))
								{
									DateTime currentTime = DateTime.Now;
									string formattedTime = currentTime.ToString("HH:mm:ss.fffff");

									writer.WriteLine(formattedTime);
								}
            }
            else
            {
                client.SetFirstPlayerKeyboardState(remoteKeyboardState);
            }
        }
    }
}
