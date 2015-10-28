#region Using Statements
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.UI;
#endregion

namespace Match3
{
    public class MessagePanel : BaseDecorator
	{
		public enum MessageType
		{
			Hide,
			Timeout,
            Win,
		};

		private MessageType type;
		private TextBlock text;


		#region Properties

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		public MessageType Type
		{
			get
			{
				return type;
			}

			set
			{
				this.type = value;

				this.IsVisible = true;

				switch (type)
				{
					case MessageType.Hide:
						this.IsVisible = false;
						break;
					case MessageType.Timeout:
						text.Text = "Game Over";
						break;
                    case MessageType.Win:
                        text.Text = "YOU WIN";
                        break;
					default:
						break;
				}
			}
		}

		/// <summary>
		/// Gets or sets the horizontal alignment.
		/// </summary>
		public HorizontalAlignment HorizontalAlignment
		{
			get
			{
				return this.entity.FindComponent<PanelControl>().HorizontalAlignment;
			}

			set
			{
				this.entity.FindComponent<PanelControl>().HorizontalAlignment = value;
			}
		}

		/// <summary>
		/// Gets or sets the vertical alignment.
		/// </summary>
		public VerticalAlignment VerticalAlignment
		{
			get
			{
				return this.entity.FindComponent<PanelControl>().VerticalAlignment;
			}

			set
			{
				this.entity.FindComponent<PanelControl>().VerticalAlignment = value;
			}
		}

		/// <summary>
		/// Gets or sets the margin.
		/// </summary>
		/// <value>
		/// The margin.
		/// </value>
		public Thickness Margin
		{
			get
			{
				return this.entity.FindComponent<PanelControl>().Margin;
			}

			set
			{
				this.entity.FindComponent<PanelControl>().Margin = value;
			}
		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="MessagePanel" /> class.
		/// </summary>
		/// <param name="type">The type.</param>
		public MessagePanel(MessageType type)
		{
			this.entity = new Entity("MessagePanel")
							   .AddComponent(new Transform2D())
							   .AddComponent(new PanelControl(1280, 720))
							   .AddComponent(new PanelControlRenderer())
							   ;
			this.text = new TextBlock("text")
			{
				Width = 132,
				Height = 42,
				Text = string.Empty,
				Margin = new Thickness(640, 360, 0,0),
				
				
			};
			this.entity.AddChild(this.text.Entity);

			this.Type = type;
		}
	}
}
