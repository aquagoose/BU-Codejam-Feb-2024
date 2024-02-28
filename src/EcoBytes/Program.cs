using EcoBytes;
using EcoBytes.Scenes;
using u4.Core;
using u4.Engine;

Logger.AttachConsole();

LaunchOptions options = LaunchOptions.Default;

GameScene scene = new GameScene();
App.Run(options, new EcoBytesGame(scene), new TestScene());