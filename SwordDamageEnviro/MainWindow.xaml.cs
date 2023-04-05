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
        SwordDamage sword = new SwordDamage();

        public MainWindow()
        {
            InitializeComponent();
            sword.SetMagic(false);
            sword.SetFlaming(false);
            // RollDice();
        }

        public void RollDice()
        {
            sword.Roll = random.Next(1, 7) + random.Next(1, 7) + random.Next(1, 7);
            sword.SetFlaming(flaming.IsChecked.Value);
            sword.SetMagic(magic.IsChecked.Value);
            DisplayDamage();
        }

        void DisplayDamage()
        {
            damage.Text = "Rolled " + sword.Roll + " for " + sword.Damage + " HP";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RollDice();
        }

        private void Flaming_Checked(object sender, RoutedEventArgs e)
        {
            sword.SetFlaming(true);
            // DisplayDamage();
        }

        private void Flaming_Unchecked(object sender, RoutedEventArgs e)
        {
            sword.SetFlaming(false);
            // DisplayDamage();
        }

        private void Magic_Checked(object sender, RoutedEventArgs e)
        {
            sword.SetMagic(true);
            // DisplayDamage();
        }

        private void Magic_Unchecked(object sender, RoutedEventArgs e)
        {
            sword.SetMagic(false);
            // DisplayDamage();
        }
    }
}

class SwordDamage
{
    public const int BASE_DAM = 3;
    public const int FLAME_DAM = 2;

    public int roll;
    public bool flaming;
    public bool magic;

    public SwordDamage(int startingRoll)
    {
        roll = startingRoll;
        CalculateDamage();
    }

    public int Roll { 
        get { return roll; }
        set
        {
            roll = value;
            CalculateDamage();
        }
    }
    // private decimal magicMult = 1M;
    // private int flamingDam = 0;
    // public int Damage;

    public int Damage { get; private set; }

    private void CalculateDamage()
    {
        decimal MagicMult = 1M;
        if (Magic) MagicMult = 1.75M;

        Damage = (int)(Roll * MagicMult) + BASE_DAM; // + flamingDam;
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

    /*public void SetMagic(bool isMagic)
    {
        if (isMagic)
        {
            magicMult = 1.75M;
        }
        else
        {
            magicMult = 1M;
        }
        CalculateDamage();
        Debug.WriteLine($"SetMagic finished: {Damage} (roll: {Roll})");
    }*/

    public bool Flaming
    {
        get { return flaming; }
        set
        {
            flaming = value;
            CalculateDamage();
        }
    }

    /*public void SetFlaming(bool isFlaming)
    {
        CalculateDamage();
        if (isFlaming)
        {
            Damage += FLAME_DAM;
        }
        Debug.WriteLine($"SetFlaming finished: {Damage} (roll: {Roll})");
    }*/
}