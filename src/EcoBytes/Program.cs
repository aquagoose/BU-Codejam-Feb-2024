using EcoBytes;
using EcoBytes.Scenes;
using u4.Core;
using u4.Engine;

Logger.AttachConsole();

LaunchOptions options = LaunchOptions.Default;

App.Run(options, new EcoBytesGame(), new GameScene());