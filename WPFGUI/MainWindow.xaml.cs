using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tidsregistrering.BLL;
using Tidsregistrering.DTO.Models;


namespace WPFGUI
{
    public partial class MainWindow : Window
    {
        private readonly AfdelingBLL bll = new AfdelingBLL();
        private readonly MedarbejderBLL medarbejderBll = new MedarbejderBLL();
        private readonly TidsregistreringBLL tidsregistreringBLL = new TidsregistreringBLL();
        private readonly SagBLL sagBLL = new SagBLL();
        public List<AfdelingDTO> Afdelinger { get; set; }
        public List<AfdelingDTO> Afdelinger2 { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            string text = "MEDARBEJDERE";
            string verticalText = string.Join("\n", text.ToCharArray());
            myTextBlock.Text = verticalText;

            string text2 = "SAGER";
            string verticalText2 = string.Join("\n", text2.ToCharArray());
            myTextBlock2.Text = verticalText2;

            Afdelinger = bll.GetAllAfdelinger();
            Afdelinger2 = Afdelinger; // de peger på samme liste, men bindings bliver adskilt
            DataContext = this;

            if (Afdelinger.Any())
                comboAfdelinger.SelectedIndex = 0;
            if (Afdelinger2.Any())
                comboAfdelinger.SelectedIndex = 0;

        }

        private void ComboAfdelinger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboAfdelinger.SelectedValue is int afdelingId)
            {
                dataGridTidsregistreringer.ItemsSource = null;
                listSager.ItemsSource = null;

                labelUgeTimer.Content = "";
                labelMaanedTimer.Content = "";
                labelTotalTimer.Content = "";

                var medarbejdere = medarbejderBll.GetMedarbejdereFraAfdeling(afdelingId);
                listMedarbejdere.ItemsSource = medarbejdere;
            }
        }

        private void ButtonTilfoejMedarbejder(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInitialer.Text) ||
                string.IsNullOrWhiteSpace(txtNavn.Text) ||
                string.IsNullOrWhiteSpace(txtCPR.Text) ||
                comboAfdelingTilMedarbejder.SelectedValue == null)
            {
                MessageBox.Show("Udfyld alle felter og vælg en afdeling.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var nyMedarbejder = new MedarbejderDTO
            {
                Initialer = txtInitialer.Text.Trim(),
                Navn = txtNavn.Text.Trim(),
                CPR = txtCPR.Text.Trim(),
                AfdelingId = (int)comboAfdelingTilMedarbejder.SelectedValue
            };

            medarbejderBll.CreateMedarbejder(nyMedarbejder);

            ComboAfdelinger_SelectionChanged(null, null);

            MessageBox.Show("Medarbejder Tilføjet!", "Bekræftelse", MessageBoxButton.OK, MessageBoxImage.Information);

            txtInitialer.Text = "";
            txtNavn.Text = "";
            txtCPR.Text = "";
            comboAfdelingTilMedarbejder.SelectedIndex = -1;
        }

        private void ButttonOpretSag(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Sag_Navn.Text) ||
                string.IsNullOrWhiteSpace(Sag_Beskrivelse.Text) ||
                comboAfdelingTilSager.SelectedValue == null)
            {
                MessageBox.Show("Udfyld alle felter og vælg en afdeling.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            var nySag = new SagDTO
            {
                Navn = Sag_Navn.Text.Trim(),
                Beskrivelse = Sag_Beskrivelse.Text.Trim(),
                AfdelingId = (int)comboAfdelingTilSager.SelectedValue
            };

            var sagBll = new SagBLL();
            sagBll.CreateSag(nySag);

            MessageBox.Show("Sagen blev oprettet med succes!", "Bekræftelse", MessageBoxButton.OK, MessageBoxImage.Information);


            Sag_Navn.Text = "";
            Sag_Beskrivelse.Text = "";
            comboAfdelingTilSager.SelectedIndex = -1;
        }

        private void ListMedarbejdere_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listMedarbejdere.SelectedItem is MedarbejderDTO valgtMedarbejder)
            {
                //Cleare felter
                dataGridTidsregistreringer.ItemsSource = null;

                labelUgeTimer.Content = "";
                labelMaanedTimer.Content = "";
                labelTotalTimer.Content = "";

                // Hent sager med tidsregistreringer for medarbejderen
                var tidsregistreringer = tidsregistreringBLL.GetTidsregistreringerForMedarbejder(valgtMedarbejder.Id);
                var sagIds = tidsregistreringer.Select(t => t.SagId).Distinct();

                var alleSager = sagBLL.GetSagerForAfdeling(valgtMedarbejder.AfdelingId);
                var sagerForMedarbejder = alleSager.Where(s => sagIds.Contains(s.Id)).ToList();

                listSager.ItemsSource = sagerForMedarbejder;
            }
        }

        private void ListBoxSager_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listSager.SelectedItem is SagDTO valgtSag && listMedarbejdere.SelectedItem is MedarbejderDTO valgtMedarbejder)
            {
                var alle = tidsregistreringBLL.GetTidsregistreringerForMedarbejder(valgtMedarbejder.Id);
                var filtrerede = alle.Where(t => t.SagId == valgtSag.Id).ToList();
                dataGridTidsregistreringer.ItemsSource = filtrerede;
            }
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if  (listMedarbejdere.SelectedItem == null || listSager.SelectedItem == null || calendar.SelectedDate == null)
                return; 

            var medarbejder = (MedarbejderDTO)listMedarbejdere.SelectedItem;
            var sag = (SagDTO)listSager.SelectedItem;
            var valgtDato = calendar.SelectedDate.Value;

            var registreringer = tidsregistreringBLL.GetTidsregistreringerForMedarbejderOgSag(medarbejder.Id, sag.Id);

            // Uge (mandag til søndag)
            var ugeStart = valgtDato.AddDays(-(int)valgtDato.DayOfWeek + (int)DayOfWeek.Monday);
            var ugeSlut = ugeStart.AddDays(7);
            var ugeTimer = registreringer
                .Where(r => r.StartTid >= ugeStart && r.StartTid < ugeSlut)
                .Sum(r => r.Timer);

            // Måned
            var månedStart = new DateTime(valgtDato.Year, valgtDato.Month, 1);
            var månedSlut = månedStart.AddMonths(1);
            var månedTimer = registreringer
                .Where(r => r.StartTid >= månedStart && r.StartTid < månedSlut)
                .Sum(r => r.Timer);

            // Total
            var totalTimer = registreringer.Sum(r => r.Timer);

            // Labels
            labelUgeTimer.Content = $"{ugeTimer:N1} timer";
            labelMaanedTimer.Content = $"{månedTimer:N1} timer";
            labelTotalTimer.Content = $"{totalTimer:N1} timer";
        }

        //Lille metode, der sørger for at man ikke skal trykke på gange på listbox efter interaktion med Calendar
        private void Calendar_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Captured is System.Windows.Controls.Primitives.CalendarItem)
            {
                Mouse.Capture(null);
            }
        }

        private void ComboAfdelinger_SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            if (comboAfdelinger2.SelectedValue is int afdelingId)
            {
                var sager = sagBLL.GetSagerForAfdeling(afdelingId);
                listSager2.ItemsSource = sager;
            }
        }
        private void ListBoxSager_SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            if (listSager2.SelectedItem is SagDTO valgtSag)
            {
                var registreringer = tidsregistreringBLL.GetTidsregistreringerForSag(valgtSag.Id);
                var samletTimer = registreringer.Sum(t => t.Timer);
                labelSamletTimer.Content = $"{samletTimer:N1} Timer";
            }
            else
            {
                labelSamletTimer.Content = "";
            }
        }
    }
}

