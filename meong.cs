using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

// --------------------------------------------------------------------------------------------
// meong 😼
// --------------------------------------------------------------------------------------------
// Targeting: Pattern Matching with Boyer-Moore
// Movement: Anti-Gravity & Stop and Go
// --------------------------------------------------------------------------------------------
/*

🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈

⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣤⣤⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣶⣿⠟⠉⠉⠻⣿⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣤⣾⠿⠉⠀⠀⠀⠀⠀⠹⣿⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣴⣾⢿
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣿⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣴⣾⡿⠛⠉⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣿⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣾⡿⠟⠁⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⣴⣿⡿⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣷⣶⣶⣦⣤⣤⣄⡀⠀⢀⣠⣾⣿⠿⠋⠀⠀⠀⠀⠀⣠⣿
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣼⣿⣿⡿⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣤⠶⠞⣿⠟⠋⠉⠉⠙⣻⠿⢿⣿⣿⣿⠟⠁⠀⠀⠀⠀⠀⢀⣿⣯⠋
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣿⠁⠛⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣰⠟⠁⢠⡞⠁⠀⠀⠀⢀⡴⠋⠀⢀⡿⠋⠁⠀⠀⠀⠀⠀⠀⠀⣾⣷⡟⠁
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣼⣿⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣼⠃⠀⢠⠏⠀⠀⠀⠀⣰⠏⠀⠀⣠⠟⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⡽⣿⣿⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣰⣿⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠠⣇⠀⣠⡏⠀⠀⠀⠀⣼⠁⠀⠀⣰⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣏⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣄⣄⣼⣿⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢻⡶⠋⠀⠀⠀⠀⢸⣧⠀⠀⣴⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⢿⣷⡀
⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⣀⠀⠀⠀⠀⣾⡿⠿⣿⡿⠁⠀⠀⠀⠀⠀⠀⣠⣶⣶⣶⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠛⠛⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠻⠃
⠀⠀⠀⠀⠀⠀⢀⣾⠿⠛⢿⣿⣷⣄⡀⣿⠃⠀⠈⠀⠀⠀⠀⠀⠀⢀⣾⣿⣿⣿⣿⣧⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⣠⣤⣦⣼⣿⠀⠀⠀⣿⣿⣿⣿⣿⣦⣀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⡿⠀⠀⠀⢀⣀⣀⣀⣤⣤⣀⣀⠀⠀⠀⠀⠀⠀⠀⠀⣤⣾⣿⣿⣿⣷⣄⠀⠀⠀⠀⠀⠀⠀⠀
⠀⣠⣾⡿⠋⠉⠉⠁⠀⠀⠀⠀⠉⢯⡙⠻⣿⣿⣷⣤⡀⠀⠀⠀⠀⢿⣿⣿⣿⣿⡿⠃⣠⡾⠟⠋⠉⠉⠉⠉⠉⠉⠛⠿⣦⠀⠀⠀⢠⣿⣿⣿⣿⣿⣿⣿⣷⡄⠀⠀⠀⠀⠀⠀⠀⠀
⣾⣿⠋⠀⠀⠀⠀⣀⣀⠀⠀⠀⠀⠀⠙⢦⣄⠉⠻⢿⣿⣷⣦⡀⠀⠈⠙⠛⠛⠋⠀⣼⠟⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠳⣕⣆⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀
⣿⡇⠀⠀⠀⣴⠟⣫⣿⣿⣄⠀⠀⠀⠀⡶⢌⡙⠶⣤⡈⠛⠿⣿⣷⣦⣀⠀⠀⠀⠀⡿⠀⢻⣷⠤⠤⣼⢷⣄⠀⠀⠀⢠⣷⠀⠘⡟⣧⠀⠻⣿⣿⣿⣿⣿⣿⣿⡟⠀⠀⠀⠀⠀⠀⠀
⣿⡇⠀⠀⢸⣟⢸⣿⣿⣿⣿⠀⠀⠀⠀⡇⠀⠈⠛⠦⣝⡳⢤⣈⠛⠻⣿⣷⣦⣀⠀⠁⠀⠀⠈⠙⠋⠁⠀⠛⠯⠭⠭⠛⠁⠀⠀⢻⠁⠀⠀⠈⠛⠿⠿⠿⠟⠋⠀⠀⠀⠀⠀⠀⠀⠀
⣿⡇⠀⠀⠈⢿⣞⣿⣿⣿⠏⠀⠀⠀⠀⡇⠀⠀⠀⠀⠀⠙⠳⢬⣛⠦⠀⠙⢻⣿⣷⣦⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣾⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⣿⡇⠀⠀⠀⠀⠉⠛⠋⠁⠀⠀⠀⠀⠀⡇⠀⠀⠀⠀⠀⠀⠀⠀⠈⠁⠀⠀⠈⣿⠉⢻⣿⣷⣦⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⠏⠀⠀⠀⠀⠀⠀⠀⢀⡶⠛⠑⣔⠖⠛⠳⣢⠀⠀
⣿⡇⠀⠀⠀⠀⠀⣠⣄⠀⠀⢰⠶⠒⠒⢧⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⠀⢸⡇⢸⡟⢿⣷⣦⣴⣶⣶⣶⣶⣤⣔⠋⠀⠀⠀⠀⠀⠀⠀⠨⣏⠀⠀⠀⠀⠀⠀⠀⣠⠟⠀⠀
⣿⡇⠀⠀⣤⠀⠀⠿⠿⠁⢀⡿⠀⠀⠀⡄⠈⠙⡷⢦⣄⡀⠀⠀⠀⠀⠀⠀⠀⣿⠀⢸⡇⢸⡇⠀⣿⠙⣿⣿⣉⠉⠙⠿⣿⣧⠀⠀⠀⠀⠀⠀⠀⠈⠙⢥⣄⠀⠀⠀⣴⠋⠀⠀⠀⠀
⣿⡇⠀⠀⠙⠷⢤⣀⣠⠴⠛⠁⠀⠀⠀⠇⠀⠀⡇⢸⡏⢹⡷⢦⣄⡀⠀⠀⠀⣿⡀⢸⡇⢸⡇⠀⡟⠀⢸⠀⢹⡷⢦⣄⣘⣿⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠓⠞⠉⠀⠀⠀⠀⠀⠀
⣿⣿⠢⣤⡀⠀⠀⠀⠀⠀⠀⣠⠾⣿⣿⡷⣤⣀⡇⠸⡇⢸⡇⢸⠉⠙⠳⢦⣄⡻⢿⣾⣧⣸⣧⠀⡇⠀⢸⠀⢸⡇⢤⣈⠙⠻⣿⣆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⢹⣿⣷⣌⡉⠛⠲⢶⣶⠖⠛⠛⢶⣄⡉⠛⠿⣽⣿⣶⣧⣸⡇⢸⠀⠀⠀⠀⠈⠙⠲⢮⣝⠻⣿⣷⣷⣄⣸⠀⢸⡇⠀⠈⠁⠀⢸⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠈⠙⠻⢿⣷⣶⣤⣉⡻⢶⣄⣀⠈⠙⠳⢦⣈⡉⠻⢿⣿⣷⣾⣦⡀⠀⠀⠀⠀⠀⠀⠈⠙⠲⢭⣛⠿⣿⣷⣼⡇⠀⠀⠀⠀⠈⣿⡇⠀⠀⠀⠀⠀⠀⣀⠀⠀⠀⠀⠀⠀⠀⠀⣀⠀
⠀⠀⠀⠀⠀⠈⠙⠻⢿⣿⣷⣶⣽⣻⡦⠀⠀⠈⠙⠷⣦⣌⡙⠻⢿⣟⣷⣤⣀⠀⠀⠀⠀⠀⠀⠀⠈⠙⠳⢯⣻⡇⠀⠀⠀⠀⠀⢸⣿⠀⣀⠀⠀⠀⠀⠈⠳⣄⣀⡀⠀⢀⣏⣽⡿⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠛⠻⢿⣿⣿⣿⣶⣤⣤⣤⣀⣈⠛⠷⣤⣈⡛⠷⢽⡻⢶⣄⣀⠀⠀⠀⠀⠀⠀⠀⠈⠛⠳⢤⣀⠀⠀⢸⣿⡙⠿⣷⣤⣄⣠⣶⡿⠉⠛⠿⠷⠿⠛⠁⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢈⣿⡿⠛⠉⠙⠛⠛⠻⢷⣦⣄⣩⣿⠶⠖⠛⠛⠛⠛⠛⠛⠿⢷⣶⣦⣄⠀⠀⠀⠀⠉⢻⣶⣿⣿⠇⠀⠀⠉⠉⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣼⣿⠁⠀⠀⠀⠀⠀⠀⠀⣿⣿⠋⠀⠀⠀⠀⠀⣠⣶⡆⠀⠀⠀⠈⠙⠿⣿⣦⡄⠀⠀⣸⣿⠟⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣿⡟⠀⠀⠀⠀⠀⠀⠀⠀⢸⡇⠀⠀⠀⠀⣰⣪⠏⠁⠀⠀⠀⠀⠀⠀⠀⠈⠛⢿⣶⣄⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⠇⠀⠀⠀⠀⠀⠀⠀⠀⢸⣧⠀⠀⢀⣾⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣴⡶⠙⢿⣿⣇⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡿⠿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⡿⠦⠹⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠻⠷⠤⠄⠙⡿⠿⠦⠤⠤⠤⠤⠄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀       

🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈

*/
// --------------------------------------------------------------------------------------------

public class Meong : Bot
{
    // Knobs
    private readonly static double  ENEMY_ENERGY_THRESHOLD = 3.5;
    private readonly static double  MOVE_WALL_MARGIN = 25;
    private readonly static double  GUN_FACTOR = 6.66;
    private readonly static double  RADAR_LOCK = 0.69;
    private readonly static double  MIN_RADIUS = 100;
    private readonly static double  DELTA_RADIUS = 100;
    private readonly static double  ITERATE_RADIUS = 3;
    private readonly static double  POINT_COUNT = 36;
    private readonly static double  MIN_DIVISOR = 1e-6;
    private readonly static double  GRAV_OVERRIDE_TRESHOLD = 0.9;
    private readonly static double  ENEMY_RADIUS = 9;
    private readonly static double  SAG_ENEMY_DISTANCE_THRESHOLD = 250;
    private readonly static double  SAG_CORNER_DISTANCE_THRESHOLD = 80;
    private readonly static int     SAG_LIMIT = 3;
    private readonly static int     PATTERN_LENGTH = 15; // Length of the pattern to match
    private readonly static int     MIN_PATTERN_LENGTH = 5; // Minimum length for matching
    private readonly static int     BULLET_OFFSET_ARENA = 50;
    private readonly static int     ENEMY_GRAVITY_CONSTANT = 300;
    private readonly static int     BULLET_GRAVITY_CONSTANT = 10;
    private readonly static int     LAST_LOC_GRAVITY_CONSTANT = 10;
    private readonly static int     CORNER_CONSTANT = 100;
    private readonly static int     SIMULATION_COUNT = 48;
    private readonly static int     ANGLE_BINS = 1080;

    // Global variables
    static double ArenaDiagonal;
    static int targetId;
    static double targetDistance;
    static double enemyDistance;
    static double pifDir;

    static double destX;
    static double destY;

    static int sag = 1;
    static int hitsag;
    static bool dontsag;

    Random rand = new Random();

    static Dictionary<int, EnemyData> enemyData = new Dictionary<int, EnemyData>();

    static List<Bullet> bullets;
    static List<MyBullet> myBullets;

    static void Main()
    {
        new Meong().Start();
    }

    Meong() : base(BotInfo.FromFile("meong.json")) { }

    public override void Run()
    {
        Console.WriteLine("Meong Meong Meong 🐱! |---| round: " + RoundNumber);
        RadarColor = Color.White;
        TracksColor = Color.White;
        GunColor = Color.White;

        ArenaDiagonal = distance(0, 0, ArenaWidth, ArenaHeight);
        SetTurnRadarRight(double.PositiveInfinity);
        AdjustGunForBodyTurn = true;
        AdjustRadarForGunTurn = true;
        AdjustRadarForBodyTurn = true;

        targetDistance = double.PositiveInfinity;
        enemyDistance = double.PositiveInfinity;
        bullets = new List<Bullet>();
        myBullets = new List<MyBullet>();
        dontsag = false;
        hitsag = 0;
        pifDir = 0;
    }

    public override void OnTick(TickEvent e)
    {
        for (int i = bullets.Count - 1; i >= 0; i--)
        {
            Bullet bullet = bullets[i];
            bullet.X += bullet.Speed * Math.Cos(bullet.Direction);
            bullet.Y += bullet.Speed * Math.Sin(bullet.Direction);
            // Graphics.DrawEllipse(new Pen(Color.Black), (float)bullet.X, (float)bullet.Y, 
            //             (float)(3 * bullet.Power), (float)(3 * bullet.Power));

            if (bullet.X < 0 - BULLET_OFFSET_ARENA || bullet.X > ArenaWidth + BULLET_OFFSET_ARENA || 
                bullet.Y < 0 - BULLET_OFFSET_ARENA || bullet.Y > ArenaHeight + BULLET_OFFSET_ARENA)
            {
                bullets.RemoveAt(i);
            }
            else 
            {
                bullets[i] = bullet;
            }
        }

        for (int i = myBullets.Count - 1; i >= 0; i--)
        {
            Bullet bullet = myBullets[i].BulletData;
            bullet.X += bullet.Speed * Math.Cos(bullet.Direction);
            bullet.Y += bullet.Speed * Math.Sin(bullet.Direction);
            // Graphics.DrawEllipse(myBullets[i].Type == 0 ? new Pen(Color.Orange) : new Pen(Color.Red), 
            //             (float)bullet.X, (float)bullet.Y, 
            //             (float)(3 * bullet.Power), (float)(3 * bullet.Power));

            EnemyData data = enemyData[myBullets[i].Target];
            if (distance(data.LastX, data.LastY, bullet.X, bullet.Y) < ENEMY_RADIUS)
            {
                data.Type[myBullets[i].Type] += 3 + (myBullets[i].Type == 0 ? 2 : 0);
                myBullets.RemoveAt(i);
            }
            else if (bullet.X < 0 - BULLET_OFFSET_ARENA || bullet.X > ArenaWidth + BULLET_OFFSET_ARENA || 
                bullet.Y < 0 - BULLET_OFFSET_ARENA || bullet.Y > ArenaHeight + BULLET_OFFSET_ARENA)
            {
                data.Type[myBullets[i].Type]--;
                myBullets.RemoveAt(i);
            }
            else 
            {
                myBullets[i].BulletData = bullet;
            }
        }

        if (hitsag > SAG_LIMIT) dontsag = true;
        if (!dontsag && EnemyCount == 1 && 
            targetDistance > SAG_ENEMY_DISTANCE_THRESHOLD && 
            distance(X, Y, 0, 0) > SAG_CORNER_DISTANCE_THRESHOLD &&
            distance(X, Y, 0, ArenaHeight) > SAG_CORNER_DISTANCE_THRESHOLD &&
            distance(X, Y, ArenaWidth, 0) > SAG_CORNER_DISTANCE_THRESHOLD &&
            distance(X, Y, ArenaWidth, ArenaHeight) > SAG_CORNER_DISTANCE_THRESHOLD 
        ) return;
        
        // Anti-Gravity
        double bestX = X;
        double bestY = Y;
        double minGrav = double.PositiveInfinity;

        for (int i = 0; i < POINT_COUNT; i++)
        {
            double theta = (2 * Math.PI / POINT_COUNT) * i;
            
            for (int u = 0; u < ITERATE_RADIUS; u++) {
                double r = Math.Sqrt(Math.Pow(u * DELTA_RADIUS, 2) + Math.Pow(MIN_RADIUS, 2));
                
                double x = X + r * Math.Cos(theta);
                double y = Y + r * Math.Sin(theta);

                if (x < MOVE_WALL_MARGIN || x > ArenaWidth - MOVE_WALL_MARGIN ||
                    y < MOVE_WALL_MARGIN || y > ArenaHeight - MOVE_WALL_MARGIN)
                {
                    continue;
                }

                double grav = CalcGrav(x, y);
                if (grav < minGrav || distance(X,Y,destX, destY) < 20)
                {
                    minGrav = grav;
                    bestX = x;
                    bestY = y;
                }
                
                // int gravColor = (int) Math.Min(255, Math.Max(0, grav * 255 / 1000));
                // Graphics.DrawEllipse(new Pen(Color.FromArgb(
                //             gravColor, 255 - gravColor, 0)), 
                //             (float) x, (float) y, 10, 10);
            }
        }

        if (minGrav < CalcGrav(destX, destY) * GRAV_OVERRIDE_TRESHOLD)
        {
            destX = bestX;
            destY = bestY;
        }

        double turn = toRad(BearingTo(destX, destY));
        SetTurnLeft(toDeg(Math.Tan(turn)));
        SetForward(DistanceTo(destX, destY) * Math.Cos(turn));

        // Anti-Gravity color
        TurretColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
        ScanColor = Color.FromArgb(105, 105, rand.Next(256));
        BodyColor = ScanColor;
        BulletColor = ScanColor;
        RadarColor = Color.White;
        TracksColor = Color.White;
        GunColor = Color.White;
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        // Update enemy data
        if (!enemyData.ContainsKey(e.ScannedBotId))
        {
            enemyData[e.ScannedBotId] = new EnemyData();
        }
        EnemyData data = enemyData[e.ScannedBotId];
        data.LastX = e.X;
        data.LastY = e.Y;
        data.IsAlive = true;
        double currentDirection = toRad(NormalizeRelativeAngle(e.Direction));
        double angularVelocity = data.HasPrevious ? 
                                (currentDirection - data.LastDirection + Math.PI) % (2 * Math.PI) - Math.PI : 0;
        data.LastDirection = currentDirection;
        double currentSpeed = e.Speed;
        double acceleration = data.HasPrevious ? currentSpeed - data.LastSpeed : 0;
        data.LastSpeed = currentSpeed;
        data.HasPrevious = true;
        
        State currentState = new State(angularVelocity, currentSpeed, acceleration);
        data.MovementHistory.Add(currentState);

        // Lock closest target
        double scannedDistance = enemyDistance = DistanceTo(e.X, e.Y);
        if (scannedDistance < targetDistance)
        {
            targetId = e.ScannedBotId;
        }
        else if (e.ScannedBotId != targetId && GunHeat != 0)
        {
            return;
        }
        targetDistance = scannedDistance;

        // Radar 
        double radarAngle = double.PositiveInfinity * NormalizeRelativeAngle(RadarBearingTo(e.X, e.Y));
        if (!double.IsNaN(radarAngle) && (GunHeat < RADAR_LOCK || EnemyCount == 1))
        {
            SetTurnRadarLeft(radarAngle);
        }

        // Fire control
        double firePower = Energy / DistanceTo(e.X, e.Y) * GUN_FACTOR;
        if (GunTurnRemaining == 0)
        {
            SetFire(firePower);
        }

        double bulletSpeed = CalcBulletSpeed(firePower);

        // Input Virtual Bullets
        double energyDrop = data.LastEnergy - e.Energy;
        data.LastEnergy = e.Energy;
        if (0.1 <= energyDrop && energyDrop <= 3)
        {
            AddVirtualBullet(e.X, e.Y, CalcBulletSpeed(energyDrop), energyDrop, (180 + DirectionTo(e.X, e.Y)));
            AddLinearVirtualBullet(e.X, e.Y, CalcBulletSpeed(energyDrop), energyDrop);
            if (!dontsag && EnemyCount == 1 && DistanceRemaining == 0)
            {
                double direction = toRad(DirectionTo(e.X, e.Y) + (90 - 15 * (targetDistance / ArenaDiagonal)) * sag);
                double distance = (3 + (int)(energyDrop * 1.999999)) * 8;
                destX = X + Math.Cos(direction) * distance;
                destY = Y + Math.Sin(direction) * distance;
                Graphics.DrawRectangle(new Pen(Color.Blue), (float)destX, (float)destY, 20, 20);
                
                if (destX < MOVE_WALL_MARGIN || destX > ArenaWidth - MOVE_WALL_MARGIN ||
                    destY < MOVE_WALL_MARGIN || destY > ArenaHeight - MOVE_WALL_MARGIN)
                {
                    sag = -sag;
                    hitsag = 0;
                }
                double turn = toRad(BearingTo(e.X, e.Y) + (90 - 15 * (targetDistance / ArenaDiagonal)) * sag);
                SetTurnLeft(toDeg(Math.Tan(turn)));
                SetForward(distance * Math.Sign(Math.Cos(turn)));
            }
        }

        // Head-on fallback
        int headon = data.Type.IndexOf(data.Type.Max());
        if (headon != 0)
        {
            BulletColor = Color.Red;
            SetTurnGunLeft(GunBearingTo(e.X, e.Y));
        }

        // --- Boyer-Moore Pattern Matching ---
        List<State> predictionSequence = new List<State>();
        
        if (data.MovementHistory.Count > PATTERN_LENGTH)
        {
            // 1. Define the pattern from the most recent movements
            var pattern = data.MovementHistory.GetRange(data.MovementHistory.Count - PATTERN_LENGTH, PATTERN_LENGTH).ToArray();

            // 2. Search for this pattern in the history (excluding the current instance)
            var historyToSearch = data.MovementHistory.GetRange(0, data.MovementHistory.Count - PATTERN_LENGTH).ToArray();

            int matchIndex = BoyerMoore.Search(historyToSearch, pattern);

            // 3. If a match is found, use the following sequence as a prediction
            if (matchIndex != -1)
            {
                int predictionStartIndex = matchIndex + pattern.Length;
                int predictionLength = Math.Min(100, data.MovementHistory.Count - predictionStartIndex); // Predict up to 100 ticks
                if (predictionLength > 0)
                {
                    predictionSequence = data.MovementHistory.GetRange(predictionStartIndex, predictionLength);
                }
            }
        }
        
        double[] angleScores = new double[ANGLE_BINS];
        for (int i = 0; i < SIMULATION_COUNT; i++)
        {
            // --- Play It Forward using Pattern Match result ---
            double predictedX = e.X;
            double predictedY = e.Y;
            double predictedDirection = currentDirection;
            double predictedSpeed = currentSpeed;
            double weight = 1.0;

            int time = 0;
            while (time * bulletSpeed < DistanceTo(predictedX, predictedY) && time < 100)
            {
                if (time < predictionSequence.Count)
                {
                    // Use the matched pattern for prediction
                    State nextState = predictionSequence[time];
                    predictedDirection += nextState.AngularVelocity / 512.0;
                    predictedSpeed = Math.Clamp(predictedSpeed + nextState.Acceleration, -MaxSpeed, MaxSpeed);
                }
                else
                {
                    // If prediction runs out, continue with last known values (or stop)
                    weight *= 0.1; // Reduce confidence if we run out of data
                }

                predictedX += predictedSpeed * Math.Cos(predictedDirection);
                predictedY += predictedSpeed * Math.Sin(predictedDirection);

                if (predictedX < 0 || predictedX > ArenaWidth ||
                    predictedY < 0 || predictedY > ArenaHeight)
                {
                    weight *= 0.01;
                }
                time++;
            }

            angleScores[(int)(((GunBearingTo(predictedX, predictedY) * ANGLE_BINS / 360) + ANGLE_BINS) % ANGLE_BINS)] += weight;
            Graphics.DrawEllipse(new Pen(Color.SkyBlue), (float)predictedX, (float)predictedY, 20, 20);
        }

        double bestAngle = 0;
        double maxScore = -1;
        for (int i = 0; i < ANGLE_BINS; i++)
        {
            if (angleScores[i] > maxScore)
            {
                maxScore = angleScores[i];
                bestAngle = i;
            }
        }

        // If no predictions were successful, fall back to head-on
        if (maxScore <= 0)
        {
            BulletColor = Color.Red;
            SetTurnGunLeft(GunBearingTo(e.X, e.Y));
        }
        else
        {
            BulletColor = Color.White;
            bestAngle = bestAngle * 360 / ANGLE_BINS;
            pifDir = toRad(bestAngle + GunDirection);
            if (headon == 0)
            {
                SetTurnGunLeft(NormalizeRelativeAngle(bestAngle));
            }
        }

        // Update other enemy positions
        foreach (var enemy in enemyData)
        {
            if (enemy.Key != targetId && enemy.Value.IsAlive)
            {
                EnemyData ed = enemy.Value;
                ed.LastX += ed.LastSpeed * Math.Cos(ed.LastDirection);
                ed.LastY += ed.LastSpeed * Math.Sin(ed.LastDirection);
            }
        }
    }


    public override void OnBulletFired(BulletFiredEvent e)
    {
        AddMyVirtualBullet(X, Y, e.Bullet.Speed, e.Bullet.Power, pifDir, targetId, 0);
        EnemyData data = enemyData[targetId];
        AddMyVirtualBullet(X, Y, e.Bullet.Speed, e.Bullet.Power, 
                        toRad(DirectionTo(data.LastX, data.LastY)), targetId, 1);
    }

    public override void OnHitByBullet(HitByBulletEvent e)
    {
        if (EnemyCount == 1)
        {
            hitsag++;
        }
    }

    public override void OnBotDeath(BotDeathEvent e)
    {
        enemyData[e.VictimId].IsAlive = false;
        
        if (e.VictimId == targetId)
        {
            targetDistance = double.PositiveInfinity;
        }
    }

    // --- Helper Functions ---
    private double CalcGrav(double candidateX, double candidateY)
    {
        double grav = 0;

        foreach (EnemyData enemy in enemyData.Values)
        {
            if (enemy.IsAlive)
            {
                grav += ENEMY_GRAVITY_CONSTANT * (enemy.LastEnergy - ENEMY_ENERGY_THRESHOLD) / 
                        (distanceSq(candidateX, candidateY, enemy.LastX, enemy.LastY) + MIN_DIVISOR);
            }
        }

        foreach (Bullet bullet in bullets)
        {
            Line2D bulletLine = new Line2D(
                bullet.X - Math.Cos(bullet.Direction) * ArenaDiagonal, 
                bullet.Y - Math.Sin(bullet.Direction) * ArenaDiagonal, 
                bullet.X + Math.Cos(bullet.Direction) * ArenaDiagonal, 
                bullet.Y + Math.Sin(bullet.Direction) * ArenaDiagonal
            );
            
            double d = bulletLine.DistanceToPoint(candidateX, candidateY);
            grav += BULLET_GRAVITY_CONSTANT * bullet.Power / (d * d + MIN_DIVISOR);

        }

        grav += LAST_LOC_GRAVITY_CONSTANT * rand.NextDouble() / 
                (Math.Pow(DistanceTo(candidateX, candidateY), 2) + MIN_DIVISOR);
        if (targetId != 0 && enemyData.ContainsKey(targetId))
            grav += targetDistance - DistanceTo(enemyData[targetId].LastX, enemyData[targetId].LastY);
            
        grav += CORNER_CONSTANT / distanceSq(candidateX, candidateY, 0, 0);
        grav += CORNER_CONSTANT / distanceSq(candidateX, candidateY, 0, ArenaHeight);
        grav += CORNER_CONSTANT / distanceSq(candidateX, candidateY, ArenaWidth, 0);
        grav += CORNER_CONSTANT / distanceSq(candidateX, candidateY, ArenaWidth, ArenaHeight);

        return grav * 1000;
    }
    
    private void AddVirtualBullet(double x, double y, double speed, double power, double direction)
    {
        Bullet bullet = new Bullet
        {
            Speed = speed,
            Direction = direction,
            X = x + 2 * speed * Math.Cos(direction),
            Y = y + 2 * speed * Math.Sin(direction),
            Power = power
        };
        bullets.Add(bullet);
    }
        
    private void AddLinearVirtualBullet(double x, double y, double speed, double power)
    {
        double vb = CalcBulletSpeed(power);
        double myDir = toRad(Direction);
        double vxt = Speed * Math.Cos(myDir);
        double vyt = Speed * Math.Sin(myDir);
        double xt = X;
        double yt = Y;
        double a = Math.Pow(vxt, 2) + Math.Pow(vyt, 2) - Math.Pow(vb, 2);
        double b = 2 * (vxt * (xt - x) + vyt * (yt - y));
        double c = Math.Pow(xt - x, 2) + Math.Pow(yt - y, 2);
        double d = Math.Pow(b, 2) - 4 * a * c;
        if (d < 0) return; // No real solution
        double t1 = (-b + Math.Sqrt(d)) / (2 * a);
        double t2 = (-b - Math.Sqrt(d)) / (2 * a);
        double t = Math.Min(Math.Max(0, t1), Math.Max(0, t2));
        double predictedX = xt + vxt * t;
        double predictedY = yt + vyt * t;
        double linearDirection = Math.Atan2(predictedY - y, predictedX - x);
        Bullet bulletLinear = new Bullet
        {
            Speed = speed,
            Direction = linearDirection,
            X = x + 2 * speed * Math.Cos(linearDirection),
            Y = y + 2 * speed * Math.Sin(linearDirection),
            Power = power * 2
        };
        bullets.Add(bulletLinear);
    }

    private void AddMyVirtualBullet(double x, double y, double speed, double power, double direction, int target, int type)
    {
        MyBullet myBullet = new MyBullet
        (
            x + 2 * speed * Math.Cos(direction),
            y + 2 * speed * Math.Sin(direction),
            speed,
            direction,
            power,
            target,
            type
        );
        myBullets.Add(myBullet);
    }
    
    public double distanceSq(double x1, double y1, double x2, double y2)
    {
        return Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2);
    }

    public double distance(double x1, double y1, double x2, double y2)
    {
        return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
    }

    public double toRad(double degree)
    {
        return degree * Math.PI / 180;
    }

    public double toDeg(double radian)
    {
        return radian * 180 / Math.PI;
    }
}

public struct State : IEquatable<State>
{
    public int AngularVelocity; // quantized: radian * 512
    public int Speed;           // -8 -- 8
    public int Acceleration;    // -1 -- 1

    public State(double angularVelocity, double speed, double acceleration)
    {
        AngularVelocity = (int)(angularVelocity * 512);
        Speed = (int)Math.Round(speed);
        
        double threshold = 0.1; 
        if (acceleration < -threshold)
            Acceleration = -1;
        else if (acceleration > threshold)
            Acceleration = 1;
        else
            Acceleration = 0;
    }
    
    public bool Equals(State other)
    {
        return AngularVelocity == other.AngularVelocity &&
               Speed == other.Speed &&
               Acceleration == other.Acceleration;
    }

    public override bool Equals(object obj)
    {
        return obj is State other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(AngularVelocity, Speed, Acceleration);
    }
}


public class EnemyData
{
    // Changed StateHistory to MovementHistory for clarity
    public List<State> MovementHistory { get; } = new List<State>();
    public List<int> Type { get; set; } = new List<int> { 13 , 0 };
    public bool HasPrevious { get; set; } = false;
    public bool IsAlive { get; set; } = true;
    public double LastDirection { get; set; }
    public double LastX { get; set; }
    public double LastY { get; set; }
    public double LastEnergy { get; set; }
    public double LastSpeed { get; set; }
}

// --- Boyer-Moore Implementation ---
public static class BoyerMoore
{
    private static Dictionary<State, int> BuildBadCharTable(State[] pattern)
    {
        var badCharTable = new Dictionary<State, int>();
        for (int i = 0; i < pattern.Length - 1; i++)
        {
            badCharTable[pattern[i]] = pattern.Length - 1 - i;
        }
        return badCharTable;
    }

    public static int Search(State[] text, State[] pattern)
    {
        if (pattern == null || pattern.Length == 0 || text == null || text.Length < pattern.Length)
        {
            return -1;
        }

        int n = text.Length;
        int m = pattern.Length;
        var badCharTable = BuildBadCharTable(pattern);
        int i = m - 1;

        // We search backwards from the last found instance
        while (i < n)
        {
            int j = m - 1;
            while (j >= 0 && text[i - (m - 1) + j].Equals(pattern[j]))
            {
                j--;
            }

            if (j < 0)
            {
                // Found a match at index i - m + 1
                // Return the last one found for most recent prediction
                return i - m + 1; 
            }

            int shift = badCharTable.GetValueOrDefault(text[i], m);
            i += shift;
        }

        return -1; // No match found
    }
}


public struct Bullet
{
    public double X;
    public double Y;
    public double Speed;
    public double Direction;
    public double Power;
}

public class MyBullet
{
    public Bullet BulletData;
    public int Target;
    public int Type;

    public MyBullet(double x, double y, double speed, double direction, double power, int target, int type)
    {
        BulletData = new Bullet { X = x, Y = y, Speed = speed, Direction = direction, Power = power };
        Target = target;
        Type = type;
    }
}

public class Line2D
{
    public double X1 { get; }
    public double Y1 { get; }
    public double X2 { get; }
    public double Y2 { get; }

    public Line2D(double x1, double y1, double x2, double y2)
    {
        X1 = x1;
        Y1 = y1;
        X2 = x2;
        Y2 = y2;
    }

    public double DistanceToPoint(double px, double py)
    {
        return Math.Abs((Y2 - Y1) * px - (X2 - X1) * py + (X2 * Y1 - Y2 * X1)) 
                / Math.Sqrt(Math.Pow(Y2 - Y1, 2) + Math.Pow(X2 - X1, 2));
    }
}
