﻿using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System.Diagnostics;

namespace Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GraphPage : ContentPage
    {
        public enum Store
        {
            Maxima = 0,
            IKI = 1,
            Rimi = 2,
            Lidl = 3,
            Norfa = 4
        }

        string path = "http://192.168.1.55:5000/api/graph/?"; //use your IP - command, ipconfig
        string item = "Pienas";
        Store storeName = Store.IKI;
        LineSeries line;
        Dictionary<DateTime, decimal> listForDays;
        List<Dictionary<DateTime, decimal>> listOfLists = new List<Dictionary<DateTime, decimal>>();

        public GraphPage()
        {
            InitializeComponent();
            AddLine();
        }
        
        public async Task GetAPIData()
        {
            try
            {
                path += "item=" + item + "&storeName=" + ((int)storeName).ToString();
                var pathh = path;
                using (var client = new RestClient(new Uri(path)))
                {
                    var request = new RestRequest(Method.GET);
                    var result = await client.Execute<List<Dictionary<DateTime, decimal>>>(request);
                    listOfLists = result.Data;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error  == " + e.Message);
            }
        }

        public async Task GetList()
        {
            await GetAPIData();
            listForDays = listOfLists[0];
            var ordered = from pair in listForDays
                          orderby pair.Key ascending
                          select pair;
            listForDays = ordered.ToDictionary(pair => pair.Key, pair => pair.Value); ;
        }

        public async void AddLine()
        {
            await GetList();
            var model = new PlotModel { Title = $"Product price change in {storeName}" };
            Content = new PlotView
            {
                Model = model,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            line = new LineSeries();

            DateTimeAxis xAxis = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "yyyy/MM/dd",

                Title = "Date",
                MinorIntervalType = DateTimeIntervalType.Days,
                IntervalType = DateTimeIntervalType.Days,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.None,
            };
            model.Axes.Add(xAxis);
            model.Axes.Add(new LinearAxis { Title = "Price" });
            foreach (var days in listForDays)
            {
                line.Points.Add(new DataPoint(DateTimeAxis.ToDouble(days.Key), Convert.ToDouble(days.Value)));
            }
            model.Series.Add(line);
        }

        async void SignUpToolbar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }
    }
}