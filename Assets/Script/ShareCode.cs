using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ShareCode : MonoBehaviour
{
	private string shareMessage;

	public void clickButton()
    {
		shareMessage = "Play this is really good";
		StartCoroutine(SocialShare());
    }

	public void WhatsappButton()
	{
		
		StartCoroutine(TakeSSAndShare());
	}
	private IEnumerator TakeSSAndShare()
	{
		yield return new WaitForEndOfFrame();

		Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		ss.Apply();

		string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
		File.WriteAllBytes(filePath, ss.EncodeToPNG());

		// To avoid memory leaks
		Destroy(ss);
        // Share on WhatsApp only, if installed (Android only)
		if( NativeShare.TargetExists( "com.whatsapp" ) )
			new NativeShare().AddFile( filePath ).SetText( "Whatsapp Share" ).SetTarget( "com.whatsapp" ).Share();
	}

	private IEnumerator SocialShare()
	{
		yield return new WaitForEndOfFrame();

		Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		ss.Apply();

		string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
		File.WriteAllBytes(filePath, ss.EncodeToPNG());

		// To avoid memory leaks
		Destroy(ss);

	   new NativeShare().AddFile(filePath).SetSubject("Share").SetText(shareMessage).Share();

		
	}
}
