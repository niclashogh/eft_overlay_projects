using System;
using System.Linq;
using Windows.Foundation;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using efto_window.ViewModels.Windows;
using efto_model.Models.Enums;
using efto.Services;
using WinRT.Interop;
using efto_window.Views.ComponentBuilders;
using System.Numerics;
using Microsoft.UI.Input;
using efto_window.Services;
using Windows.UI;
using efto_model.Models;
using System.Threading.Tasks;
using efto_model.Models.Extractions;
using efto_model.Models.Quests;
using efto_model.Models.Base;
using efto_model.Records;
using efto_model.Interfaces;

namespace efto_window.Views.Windows
{
    public sealed partial class Map_Window : Window
    {
        private MapVM viewModel { get; set; } = new();
        private WindowController controller;

        #region Variables & Properties
        private double currentZoom { get; set; } = 1;
        private static double ZOOM_STEP = .025;
        private static double MIN_ZOOM = 1;
        private static double MAX_ZOOM = 5;

        private DimensionRecord<uint> rawImageSize;
        private double rawImageRatio;

        private Draggable_Objects selectedDraggableObject;
        private bool isDraggingObject = false;
        private Point initialMousePosition;
        #endregion

        public Map_Window()
        {
            this.InitializeComponent();
            this.PARENT_GRID.DataContext = this.viewModel;

            // Window styling
            this.controller = new(this.AppWindow, 1800);
            this.controller.ConfigureTitleBar(this);
            this.controller.SetTopMost(WindowNative.GetWindowHandle(this));

            // Window events
            this.Closed += (sender, e) => this.controller.OnClose(InterProcessComs.Map);
            this.SizeChanged += (sender, e) => OnSizeChanged();

            // VM events
            this.viewModel.PropertyChanged += (sender, e) =>
            {
                // Condition => () => Executalbe || Condition => Lambda => Executalbe
                // Passing it through a lambda function, which returns 'Action' and handles the execution
                // Use Func<Task>? to handle methods, which returns 'Task' AND should be awaited.

                Action? method = e.PropertyName switch
                {
                    nameof(this.viewModel.SelectedMap) => () => OnMapChanged(),
                    nameof(this.viewModel.FinishedLoadingExtractions) => () => _ = CreateExtractions(),
                    nameof(this.viewModel.FinishedLoadingTasks) => () => _ = CreateTasks(),
                    nameof(this.viewModel.FinishedLoadingBTR) => () => _ = CreateBTRs(),
                    nameof(this.viewModel.FinishedLoadingMarkers) => () => _ = CreateMarkers(),
                    _ => null
                };

                if (method != null)
                {
                    method.Invoke();
                }
            };

            // Map events
            this.MAP_INNER_CANVAS.PointerWheelChanged += (sender, e) => ScrollEventController<Canvas>(sender, e);
            this.MAP_INNER_CANVAS.PointerPressed += (sender, e) => CaptureEventController<Canvas>(sender, e, Draggable_Objects.Map);
            this.MAP_INNER_CANVAS.PointerMoved += (sender, e) => MoveEventController<Canvas>(sender, e);
            this.MAP_INNER_CANVAS.PointerReleased += (sender, e) => ReleaseEventController<Canvas>(sender, e);
        }

        #region OnChanged
        private async void OnSliderChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (sender is Slider slider)
            {
                Grid? parent = FrameworkElementService.FindParentByType<Grid>(slider);

                if (parent != null)
                {
                    FrameworkElement? shape = FrameworkElementService.FindChildByTag(parent, "MARKER_ICON");

                    if (shape != null)
                    {
                        if (slider.Tag.ToString() == "MARKER_SLIDER_WIDTH")
                        {
                            shape.Width = e.NewValue;
                            Canvas.SetLeft(parent, Canvas.GetLeft(parent) + (e.NewValue / 2));
                        }
                        else if (slider.Tag.ToString() == "MARKER_SLIDER_HEIGHT")
                        {
                            shape.Height = e.NewValue;
                            Canvas.SetTop(parent, Canvas.GetTop(parent) + (e.NewValue / 2));
                        }


                        if (int.TryParse(parent.Tag.ToString(), out int id))
                        {
                            _ = this.viewModel.UpdateMarkerSize(new DimensionRecord<double>(shape.Width, shape.Height), id);
                        }
                    }
                }
            }
        }

        private async void OnMapChanged()
        {
            this.rawImageSize = await ImageService.GetDimensions(ImageFolders.Maps, this.viewModel.SelectedMap.ToString());
            this.rawImageRatio = (double)rawImageSize.Width / (double)rawImageSize.Height;

            ResetImageCanvas();
            OnSizeChanged();
        }

        private void OnSizeChanged()
        {
            // Get new window size
            DimensionRecord<double> windowSize = new(this.Bounds.Width, this.Bounds.Height);
            double windowRatio = windowSize.Width / windowSize.Height;

            if (this.rawImageRatio > 1) // Image is longer
            {
                if (this.rawImageRatio > windowRatio) // Image is longer than window
                {
                    FitImageByWidth(windowSize);
                }
                else // Window is longer than image
                {
                    FitImageByHeight(windowSize);
                }
            }
            else // Image is taller
            {
                if (this.rawImageRatio < windowRatio) // Image is taller than window
                {
                    FitImageByHeight(windowSize);
                }
                else // Window is taller than image
                {
                    FitImageByWidth(windowSize);
                }
            }
        }

        private void ResetImageCanvas()
        {
            this.MAP_TRANSFORM.ScaleX = 1;
            this.MAP_TRANSFORM.ScaleY = 1;
            this.MAP_TRANSFORM.TranslateX = 0;
            this.MAP_TRANSFORM.TranslateY = 0;
            this.currentZoom = 1;

            Canvas.SetTop(this.MAP_INNER_CANVAS, 0);
            Canvas.SetLeft(this.MAP_INNER_CANVAS, 0);
        }

        private void FitImageByWidth(DimensionRecord<double> windowSize)
        {
            this.MAP_IMAGE.Width = windowSize.Width;
            this.MAP_IMAGE.Height = windowSize.Width / this.rawImageRatio;
            double newTop = (windowSize.Height / 2) - ((windowSize.Width / this.rawImageRatio) / 2);

            Canvas.SetTop(this.MAP_INNER_CANVAS, newTop);
            Canvas.SetLeft(this.MAP_INNER_CANVAS, 0);
        }

        private void FitImageByHeight(DimensionRecord<double> windowSize)
        {
            this.MAP_IMAGE.Height = windowSize.Height;
            this.MAP_IMAGE.Width = windowSize.Height * this.rawImageRatio;
            double newLeft = (this.Bounds.Width / 2) - ((this.Bounds.Height * this.rawImageRatio) / 2);

            Canvas.SetTop(this.MAP_INNER_CANVAS, 0);
            Canvas.SetLeft(this.MAP_INNER_CANVAS, newLeft);
        }
        #endregion

        #region Create POIs
        private async Task CreateExtractions()
        {
            if (this.viewModel.Extractions != null && this.viewModel.Extractions.Count > 0)
            {
                foreach(Extraction_DTO extraction in this.viewModel.Extractions)
                {
                    ExtractionComponent component = new(extraction);

                    if (this.viewModel.IsRunningAsAdminstrator())
                    {
                        component.GRID.PointerPressed += (sender, e) => CaptureEventController<Grid>(sender, e, Draggable_Objects.Extraction);
                        component.GRID.PointerMoved += (sender, e) => MoveEventController<Grid>(sender, e);
                        component.GRID.PointerReleased += (sender, e) => ReleaseEventController<Grid>(sender, e);
                    }

                    this.MAP_INNER_CANVAS.Children.Add(component.GRID);

                    double centerPercentageX = extraction.X * this.MAP_INNER_CANVAS.Width - component.Size.Width / 2;
                    double centerPercentageY = extraction.Y * this.MAP_INNER_CANVAS.Height - component.Size.Height / 2;
                    Canvas.SetLeft(component.GRID, centerPercentageX);
                    Canvas.SetTop(component.GRID, centerPercentageY);
                    Canvas.SetZIndex(component.GRID, 2);
                }
            }
        }

        private async Task CreateTasks()
        {
            if (this.viewModel.Tasks != null && this.viewModel.Tasks.Count > 0)
            {
                int colorIndex = 0;
                int prevQuestId = this.viewModel.Tasks.First().QuestId;

                foreach(Quest_Task task in this.viewModel.Tasks)
                {
                    if (task.QuestId != prevQuestId && colorIndex < Color_DTO.Colors.Count - 1)
                    {
                        prevQuestId = task.QuestId;
                        colorIndex++;
                    }

                    Color componentColor = Color.FromArgb(255, Color_DTO.Colors[colorIndex].R, Color_DTO.Colors[colorIndex].B, Color_DTO.Colors[colorIndex].G);
                    TaskComponent component = new(task, componentColor);

                    if (task.DP == Dragging_Privileges.Everyone || this.viewModel.IsRunningAsAdminstrator())
                    {
                        component.GRID.PointerPressed += (sender, e) => CaptureEventController<Grid>(sender, e, Draggable_Objects.QuestTask);
                        component.GRID.PointerMoved += (sender, e) => MoveEventController<Grid>(sender, e);
                        component.GRID.PointerReleased += (sender, e) => ReleaseEventController<Grid>(sender, e);
                    }

                    this.MAP_INNER_CANVAS.Children.Add(component.GRID);

                    double centerPercentageX = task.X * this.MAP_INNER_CANVAS.Width - component.Size.Width / 2;
                    double centerPercentageY = task.Y * this.MAP_INNER_CANVAS.Height - component.Size.Height / 2;
                    Canvas.SetLeft(component.GRID, centerPercentageX);
                    Canvas.SetTop(component.GRID, centerPercentageY);
                    Canvas.SetZIndex(component.GRID, 2);
                }
            }
        }

        private async Task CreateBTRs()
        {
            if (this.viewModel.BTR != null && this.viewModel.BTR.Count > 0)
            {
                foreach(BTR btr in this.viewModel.BTR)
                {
                    BTRComponent component = new(btr);

                    if (btr.DP == Dragging_Privileges.Everyone || this.viewModel.IsRunningAsAdminstrator())
                    {
                        component.GRID.PointerPressed += (sender, e) => CaptureEventController<Grid>(sender, e, Draggable_Objects.BTR);
                        component.GRID.PointerMoved += (sender, e) => MoveEventController<Grid>(sender, e);
                        component.GRID.PointerReleased += (sender, e) => ReleaseEventController<Grid>(sender, e);
                    }

                    this.MAP_INNER_CANVAS.Children.Add(component.GRID);

                    double centerPercentageX = btr.X * this.MAP_INNER_CANVAS.Width - component.Size.Width / 2;
                    double centerPercentageY = btr.Y * this.MAP_INNER_CANVAS.Height - component.Size.Height / 2;
                    Canvas.SetLeft(component.GRID, centerPercentageX);
                    Canvas.SetTop(component.GRID, centerPercentageY);
                    Canvas.SetZIndex(component.GRID, 2);
                }
            }
        }

        private async Task CreateMarkers()
        {
            if (this.viewModel.Markers != null && this.viewModel.Markers.Count > 0)
            {
                foreach(Marker marker in this.viewModel.Markers)
                {
                    MarkerComponent component = new(marker, OnSliderChanged);

                    if (marker.DP == Dragging_Privileges.Everyone || this.viewModel.IsRunningAsAdminstrator())
                    {
                        component.GRID.PointerPressed += (sender, e) => CaptureEventController<Grid>(sender, e, Draggable_Objects.Marker);
                        component.GRID.PointerMoved += (sender, e) => MoveEventController<Grid>(sender, e);
                        component.GRID.PointerReleased += (sender, e) => ReleaseEventController<Grid>(sender, e);
                    }

                    this.MAP_INNER_CANVAS.Children.Add(component.GRID);

                    double centerPercentageX = marker.X * this.MAP_INNER_CANVAS.Width - marker.Width / 2;
                    double centerPercentageY = marker.Y * this.MAP_INNER_CANVAS.Height - marker.Height / 2;
                    Canvas.SetLeft(component.GRID, centerPercentageX);
                    Canvas.SetTop(component.GRID, centerPercentageY);
                    Canvas.SetZIndex(component.GRID, 1);
                }
            }
        }
        #endregion

        #region Event Controllers // TODO: Scale POIs down when zoom-in and vice-versa
        private async void ScrollEventController<T>(object sender , PointerRoutedEventArgs e) where T : FrameworkElement
        {
            if (sender is T frameworkElement && frameworkElement != null)
            {
                PointerPoint pointer = e.GetCurrentPoint(frameworkElement);
                Point pos = new(pointer.Position.X, pointer.Position.Y);

                int delta = pointer.Properties.MouseWheelDelta;
                double zoomFactor = delta > 0 ? (1 + ZOOM_STEP) : (1 - ZOOM_STEP);
                double newZoom = Math.Clamp(this.currentZoom * zoomFactor, MIN_ZOOM, MAX_ZOOM);
                double zoomChange = newZoom / this.currentZoom;

                if (newZoom == 1) // Reset when fully zoomed out
                {
                    ResetImageCanvas();
                    OnSizeChanged();
                }
                else
                {
                    this.MAP_TRANSFORM.TranslateX = pos.X - (pos.X - this.MAP_TRANSFORM.TranslateX) * zoomChange;
                    this.MAP_TRANSFORM.TranslateY = pos.Y - (pos.Y - this.MAP_TRANSFORM.TranslateY) * zoomChange;
                    
                    this.MAP_TRANSFORM.ScaleX = newZoom;
                    this.MAP_TRANSFORM.ScaleY = newZoom;
                    this.currentZoom = newZoom;
                }
            }
        }

        private async void CaptureEventController<T>(object sender, PointerRoutedEventArgs e, Draggable_Objects dragging) where T : FrameworkElement
        {
            if (sender is T frameworkElement && frameworkElement != null)
            {
                this.selectedDraggableObject = dragging;

                this.initialMousePosition = e.GetCurrentPoint(frameworkElement).Position;
                frameworkElement.CapturePointer(e.Pointer);
                this.isDraggingObject = true;
            }
        }

        private async void MoveEventController<T>(object sender, PointerRoutedEventArgs e) where T : FrameworkElement
        {
            if (this.isDraggingObject)
            {
                if (sender is T frameworkElement && frameworkElement != null)
                {
                    Point newMousePos = e.GetCurrentPoint(frameworkElement).Position;
                    Vector2 delta = new((float)(newMousePos.X - this.initialMousePosition.X), (float)(newMousePos.Y - this.initialMousePosition.Y));

                    double leftCoordinate = Canvas.GetLeft(frameworkElement);
                    leftCoordinate = double.IsNaN(leftCoordinate) ? 0 : leftCoordinate;

                    double topCoordinate = Canvas.GetTop(frameworkElement);
                    topCoordinate = double.IsNaN(topCoordinate) ? 0 : topCoordinate;

                    Canvas.SetLeft(frameworkElement, leftCoordinate + delta.X);
                    Canvas.SetTop(frameworkElement, topCoordinate + delta.Y);

                    this.initialMousePosition = newMousePos;
                }
            }
        }

        private async void ReleaseEventController<T>(object sender, PointerRoutedEventArgs e) where T : FrameworkElement
        {
            if (sender is T frameworkElement && frameworkElement != null)
            {
                if (frameworkElement.Tag != null)
                {
                    if (int.TryParse(frameworkElement.Tag.ToString(), out int id))
                    {
                        double centerPercentageX = (Canvas.GetLeft(frameworkElement) + this.MAP_TRANSFORM.CenterX) / this.MAP_INNER_CANVAS.Width;
                        double centerPercentageY = (Canvas.GetTop(frameworkElement) + this.MAP_TRANSFORM.CenterY) / this.MAP_INNER_CANVAS.Height;
                        PositionRecord<double, double> position = new(centerPercentageX, centerPercentageY);

                        if (this.selectedDraggableObject == Draggable_Objects.Extraction)
                        {
                            Extraction extraction = new(position);
                            extraction.Id = id;

                            _ = this.viewModel.UpdateExtraction(extraction);
                        }
                        else if (this.selectedDraggableObject == Draggable_Objects.QuestTask)
                        {
                            Quest_Task task = new(position);
                            task.Id = id;

                            _ = this.viewModel.UpdateQuestTask(task);
                        }
                        else if (this.selectedDraggableObject == Draggable_Objects.BTR)
                        {
                            BTR btr = new(position);
                            btr.Id = id;

                            _ = this.viewModel.UpdateBTR(btr);
                        }
                        else if (this.selectedDraggableObject == Draggable_Objects.Marker)
                        {
                            Marker marker = new(position);
                            marker.Id = id;

                            _ = this.viewModel.UpdateMarkerSize(marker);
                        }
                    }
                }

                this.isDraggingObject = false;
                frameworkElement.ReleasePointerCapture(e.Pointer);
            }
        }
        #endregion

        private void Menu_Btn_Click(object sender, RoutedEventArgs e) => this.controller.Menu_Toggle(this.MENU_SPLITVIEW);
    }
}
