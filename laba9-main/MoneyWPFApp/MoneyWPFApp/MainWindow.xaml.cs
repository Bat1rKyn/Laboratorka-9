using System;
using System.Windows;

namespace MoneyWPFApp
{
    public partial class MainWindow : Window
    {
        private Money _money1;
        private Money _money2;
        private uint _currentNumber;

        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            _money1 = new Money(0, 0);
            _money2 = new Money(0, 0);
            _currentNumber = 0;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            TxtCurrentMoney1.Text = $"Сумма 1:" +
                $" {_money1.ToString()}";
            TxtCurrentMoney2.Text = $"Сумма 2:" +
                $" {_money2.ToString()}";
        }

        private bool Read_money1()
        {
            try
            {
                uint rubles = uint.Parse(TxtRubles1.Text);
                byte kopeks = byte.Parse(TxtKopeks1.Text);

                // Убрали проверку на kopeks > 99
                _money1 = new Money(rubles, kopeks);
                UpdateDisplay();

                // Обновляем текстовые поля с нормализованными значениями
                TxtRubles1.Text = _money1.Rubles.ToString();
                TxtKopeks1.Text = _money1.Kopeks.ToString();

                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка ввода суммы 1", "Ошибка",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
                return false;
            }
        }

        private bool Read_money2()
        {
            try
            {
                uint rubles = uint.Parse(TxtRubles2.Text);
                byte kopeks = byte.Parse(TxtKopeks2.Text);

                // Убрали проверку на kopeks > 99
                _money2 = new Money(rubles, kopeks);
                UpdateDisplay();

                // Обновляем текстовые поля с нормализованными значениями
                TxtRubles2.Text = _money2.Rubles.ToString();
                TxtKopeks2.Text = _money2.Kopeks.ToString();

                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка ввода суммы 2", "Ошибка",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
                return false;
            }
        }

        private bool ReadNumber()
        {
            try
            {
                _currentNumber = uint.Parse(TxtNumber.Text);
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка ввода числа", "Ошибка",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
                return false;
            }
        }

        private void ClearResults()
        {
            TxtConstructors.Text = "";
            TxtBinaryResult.Text = "";
            TxtUnaryResult.Text = "";
        }

        // Тест конструкторов
        private void BtnConstructors_Click(object sender,
            RoutedEventArgs e)
        {
            ClearResults();

            Money defaultMoney = new Money();
            Money paramMoney = new Money(15, 75);
            Money copyMoney = new Money(paramMoney);

            TxtConstructors.Text = $"Конструктор по умолчанию: " +
                $"{defaultMoney.ToString()}\r\n" +
                                   $"Конструктор с параметрами " +
                                   $"(15, 75): {paramMoney.ToString()}\r\n" +
                                   $"Конструктор копирования: " +
                                   $"{copyMoney.ToString()}";
        }

        // Money - Money
        private void BtnMoneyMinusMoney_Click
            (object sender, RoutedEventArgs e)
        {
            ClearResults();

            if (!Read_money1())
                return;
            if (!Read_money2())
                return;

            try
            {
                Money result = _money1 - _money2;
                TxtBinaryResult.Text = $"{_money1.ToString()} - " +
                    $"{_money2.ToString()} = {result.ToString()}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                TxtBinaryResult.Text = $"Ошибка: {ex.Message}";
            }
        }

        // Money - uint
        private void BtnMoneyMinusUint_Click
            (object sender, RoutedEventArgs e)
        {
            ClearResults();

            if (!Read_money1())
            {
                return;
            }
            if (!ReadNumber())
            {
                return;
            }

            try
            {
                Money result = _money1 - _currentNumber;
                TxtBinaryResult.Text = $"{_money1.ToString()} -" +
                    $" {_currentNumber} " +
                    $"руб. = {result.ToString()}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                TxtBinaryResult.Text = $"Ошибка: {ex.Message}";
            }
        }

        // uint - Money
        private void BtnUintMinusMoney_Click(object sender,
            RoutedEventArgs e)
        {
            ClearResults();

            if (!ReadNumber())
            {
                return;
            }
            if (!Read_money1())
            {
                return;
            }

            try
            {
                Money result = _currentNumber - _money1;
                TxtBinaryResult.Text = $"{_currentNumber} руб. -" +
                    $" {_money1.ToString()} = " +
                    $"{result.ToString()}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                TxtBinaryResult.Text = $"Ошибка: {ex.Message}";
            }
        }

        // Оператор ++
        private void BtnIncrement_Click(object sender, RoutedEventArgs e)
        {
            ClearResults();

            if (!Read_money1())
            {
                return;
            }

            try
            {
                _money1++;
                TxtUnaryResult.Text = $"После ++: {_money1.ToString()}";
                UpdateDisplay();
                TxtRubles1.Text = _money1.Rubles.ToString();
                TxtKopeks1.Text = _money1.Kopeks.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        // Оператор --
        private void BtnDecrement_Click(object sender, RoutedEventArgs e)
        {
            ClearResults();

            if (!Read_money1()) return;

            try
            {
                _money1--;
                TxtUnaryResult.Text = $"После --: {_money1.ToString()}";
                UpdateDisplay();
                TxtRubles1.Text = _money1.Rubles.ToString();
                TxtKopeks1.Text = _money1.Kopeks.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }
    }
}