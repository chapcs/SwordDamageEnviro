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
using System.Diagnostics;

namespace SwordDamageEnviro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();
        SwordDamage sword;

        public MainWindow()
        {
            InitializeComponent();
            sword = new SwordDamage(RollDice);
        }

        public void RollDice()
        {
            sword.Roll = random.Next(1, 7) + random.Next(1, 7) + random.Next(1, 7);
            DisplayDamage();
        }

        void DisplayDamage()
        {
            damage.Text = $"Rolled {sword.Roll} for {sword.Damage} HP";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RollDice();
        }

        private void Flaming_Checked(object sender, RoutedEventArgs e)
        {
            sword.Flaming = true;
            DisplayDamage();
        }

        private void Flaming_Unchecked(object sender, RoutedEventArgs e)
        {
            sword.Flaming = false;
            DisplayDamage();
        }

        private void Magic_Checked(object sender, RoutedEventArgs e)
        {
            sword.Magic = true;
            DisplayDamage();
        }

        private void Magic_Unchecked(object sender, RoutedEventArgs e)
        {
            sword.Magic = false;
            DisplayDamage();
        }
    }
}

/// <summary>
/// Main method that calculates the the sword damage based off different params
/// </summary>
class SwordDamage
{
    public const int BASE_DAM = 3;
    public const int FLAME_DAM = 2;

    public int roll;
    public bool flaming;
    public bool magic;

    public int Damage { get; private set; }

    public SwordDamage(int startingRoll)
    {
        roll = startingRoll;
        CalculateDamage();
    }

    public int Roll
    {
        get { return roll; }
        set
        {
            roll = value;
            CalculateDamage();
        }
    }

    private void CalculateDamage()
    {
        decimal MagicMult = 1M;
        if (Magic) MagicMult = 1.75M;

        Damage = (int)(Roll * MagicMult) + BASE_DAM;
        if (Flaming) Damage += FLAME_DAM;
        Debug.WriteLine($"CalculateDamage finished: {Damage} (roll: {Roll})");
    }
    public bool Magic
    {
        get { return magic; }
        set
        {
            magic = value;
            CalculateDamage();
        }
    }
    public bool Flaming
    {
        get { return flaming; }
        set
        {
            flaming = value;
            CalculateDamage();
        }
    }
}