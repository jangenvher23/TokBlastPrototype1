using CocosSharp;
using System;
using System.Linq;
using System.Reflection;

namespace TokBlastPrototype1.Extensions
{
	public static class CCNodeExtension
	{
		public static void PositionCenter(this CCNode target, CCNode parent)
		{
			var bounds = parent.VisibleBoundsWorldspace;
			target.Position = bounds.Center;
		}

		public static bool IsTouched(this CCNode target, CCPoint touchPoint)
		{
			if (target.BoundingBoxTransformedToWorld.ContainsPoint(touchPoint))
			{
				return true;
			}
			return false;
		}

		public static void SetTouchListener(this CCNode target,
			Action<object, CCTouch, CCEvent> beganAction,
			Action<object, CCTouch, CCEvent> endedAction = null,
			Action<object, CCTouch, CCEvent> movedAction = null)
		{
			var releasedColor = App.PrimaryTextColor;
			var pressedColor = App.PressedColor;

			var margin = 0;
			var buttonCenterX = target.PositionX + margin;
			var buttonCenterY = target.PositionY + margin;

			var touchListener = new CCEventListenerTouchOneByOne();
			touchListener.OnTouchBegan += (CCTouch touch, CCEvent touchEvent) =>
			{
				if (!target.IsTouched(touch.Location))
					return false;

				// Pressed
				target.SetDisplayedColorFromHex(pressedColor);

				if (beganAction != null)
				{
					beganAction?.Invoke(target, touch, touchEvent);
				}
				return true;
			};
			touchListener.OnTouchEnded += (CCTouch touch, CCEvent touchEvent) =>
			{
				if (!target.IsTouched(touch.Location))
					return;

				// Released
				target.SetDisplayedColorFromHex(releasedColor);

				if (endedAction != null)
				{
					endedAction?.Invoke(target, touch, touchEvent);
				}
			};
			touchListener.OnTouchMoved += (CCTouch touch, CCEvent touchEvent) =>
			{
				if (target.IsTouched(touch.Location))
				{
					// Pressed
					target.SetDisplayedColorFromHex(pressedColor);
				}
				else
				{
					// Released
					target.SetDisplayedColorFromHex(releasedColor);
				}

				if (movedAction != null)
				{
					movedAction?.Invoke(target, touch, touchEvent);
				}
			};
			target.AddEventListener(touchListener);
		}

		public static CCColor3B ToCCColor3B(this string target)
		{
			return new CCColor3B(target.ToCCColor4B());
		}

		public static CCColor4B ToCCColor4B(this string target)
		{
			var color = Xamarin.Forms.Color.FromHex(target);
			return new CCColor4B((float)color.R, (float)color.G, (float)color.B, (float)color.A);
		}

		public static void SetDisplayedColorFromHex(this CCNode target, string hexString)
		{
			target.UpdateDisplayedColor(hexString.ToCCColor3B());
		}

		public static bool HasSameDisplayedColor(this CCNode target, string hexString)
		{
			if (target.DisplayedColor.Equals(hexString.ToCCColor3B()))
				return true;
			return false;
		}

		public static CCNode DeepClone(this CCNode target)
		{
			CCNode node = new CCNode();

			var nodeType = node.GetType();
			var nodeProperties = nodeType.GetRuntimeProperties();
			foreach (PropertyInfo nodePropertyInfo in nodeProperties)
			{
				if (!nodePropertyInfo.CanRead)
					continue;

				var targetType = target.GetType();
				var targetProperties = targetType.GetRuntimeProperties();
				foreach (PropertyInfo targetPropertyInfo in targetProperties)
				{
					if (!targetPropertyInfo.CanRead)
						continue;

					if (nodePropertyInfo.Name.Equals(targetPropertyInfo.Name))
					{
						var targetMember = targetType.GetRuntimeProperty(targetPropertyInfo.Name);
						var targetValue = targetMember.GetValue(target);
						if (nodePropertyInfo.SetMethod != null)
						{
							if (targetValue != null)
								nodePropertyInfo.SetValue(node, targetValue);
						}
					}
				}
			}

			return node;
		}

		public static object CloneObject(this object target)
		{
			// Get the type of source object and create a new instance of that type 
			Type typeSource = target.GetType();
			object objTarget = Activator.CreateInstance(typeSource);

			// Get all the properties of source object type
			PropertyInfo[] propertyInfo = typeSource.GetRuntimeProperties().ToArray();

			// Assign all source property to taget object 's properties
			foreach (PropertyInfo property in propertyInfo)
			{
				// Check whether property can be written to
				if (property.CanWrite)
				{
					if (property.PropertyType.GetTypeInfo().IsValueType || property.PropertyType.GetTypeInfo().IsEnum || property.PropertyType.Equals(typeof(string)))
					{
						// Check whether property type is value type, enum or string type 
						property.SetValue(objTarget, property.GetValue(target, null), null);
					}
					else
					{
						// Else property type is object/complex types, so need to recursively call this method until the end of the tree is reached
						object objPropertyValue = property.GetValue(target, null);
						if (objPropertyValue == null)
						{
							property.SetValue(objTarget, null, null);
						}
						else
						{
							property.SetValue(objTarget, objPropertyValue.CloneObject(), null);
						}
					}
				}
			}

			return objTarget;
		}
	}
}
