namespace DrawableSample;

public sealed class VerticalProgressLine : GraphicsView, IDrawable
{
    public VerticalProgressLine()
    {
        Drawable = this;
    }

    public static readonly BindableProperty StrokeWidthProperty = BindableProperty.Create(nameof(StrokeWidth), typeof(float), typeof(VerticalProgressLine), 10f, propertyChanged: OnPropertyChanged);
    public float StrokeWidth
    {
        get => (float)GetValue(StrokeWidthProperty);
        set => SetValue(StrokeWidthProperty, value);
    }

    public static readonly BindableProperty MaxProperty = BindableProperty.Create(nameof(Max), typeof(int), typeof(VerticalProgressLine), 5, propertyChanged: OnPropertyChanged);
    public int Max
    {
        get => (int)GetValue(MaxProperty);
        set => SetValue(MaxProperty, value);
    }

    public static readonly BindableProperty ProgressProperty = BindableProperty.Create(nameof(Progress), typeof(float), typeof(VerticalProgressLine), 0f, propertyChanged: OnPropertyChanged);
    public float Progress
    {
        get => (float)GetValue(ProgressProperty);
        set => SetValue(ProgressProperty, value);
    }

    public static readonly BindableProperty ProgressColorProperty = BindableProperty.Create(nameof(ProgressColor), typeof(Color), typeof(VerticalProgressLine), Colors.Blue, propertyChanged: OnPropertyChanged);
    public Color ProgressColor
    {
        get => (Color)GetValue(ProgressColorProperty);
        set => SetValue(ProgressColorProperty, value);
    }

    public static readonly BindableProperty BaseColorProperty = BindableProperty.Create(nameof(BaseColor), typeof(Color), typeof(VerticalProgressLine), Colors.Red, propertyChanged: OnPropertyChanged);
    public Color BaseColor
    {
        get => (Color)GetValue(BaseColorProperty);
        set => SetValue(BaseColorProperty, value);
    }

    private static void OnPropertyChanged(BindableObject bindable, object oldVal, object newVal)
    {
        var line = bindable as VerticalProgressLine;
        line?.Invalidate();
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        float x1 = 0f;
        float x2 = 0f;
        var y1 = (float)HeightRequest;
        float y2 = 0f;

        PointF point1 = new(x1, y1);
        PointF point2 = new(x2, y2);

        if (Progress < 0)
        {
            Progress = 0;
        }
        else if (Progress > Max)
        {
            Progress = Max;
        }

        if (Progress < Max)
        {
            var progressHeight = Progress * HeightRequest / Max;

            if (progressHeight > 0)
            {
                point2 = new(x2, y1 - (float)progressHeight);

                canvas.StrokeColor = ProgressColor;
                canvas.StrokeSize = StrokeWidth;
                canvas.DrawLine(point1, point2);

                point1 = new(x1, y1 - (float)progressHeight);
                point2 = new(x1, y2);
            }

            canvas.StrokeColor = BaseColor;
            canvas.StrokeSize = StrokeWidth;
            canvas.DrawLine(point1, point2);
        }
        else
        {
            canvas.StrokeColor = ProgressColor;
            canvas.StrokeSize = StrokeWidth;
            canvas.DrawLine(point1, point2);
        }
    }
}