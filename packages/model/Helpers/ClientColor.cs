namespace Anovase.Sunnyside.Helpers;

public class ClientColor
{
	public double Hue { get; set; }
	public double Saturation { get; set; }

	/// <summary>
	/// Lightness Positive Deviation.
	/// Positive means it's cisdirectional to the theme multiplier and higher values makes it more close to the theme base colour.
	/// </summary>
	/// <value></value>
	public double Lpd { get; set; }


	/// <summary>
	/// Calculate the lightness of the colour within a theme.
	/// </summary>
	/// <param name="tldm">Theme Lightness Direction Multiplier. The lightness multiplier for the current colour theme. +1 for light, -1 for dark.</param>
	/// <returns></returns>
	public double CalculateLightness(int tldm)
		=> 50.0 + tldm * Lpd;

	public int CalculateForegroundLightnessDirection(int tldm)
		=> (int)(tldm * .016 * Lpd + .003 * Saturation) * 2 + 1;
}
