using UnityEngine;
using UnityEngine.UI;

public class JumpRopeScript : MonoBehaviour
{
	public Text jumpCount;

	public Animator rope;

	public CameraScript cs;

	public PlayerScript ps;

	public PlaytimeScript playtime;

	public int jumps;

	public float jumpDelay;

	public float ropePosition;

	public bool ropeHit;

	public bool jumpStarted;

	private void OnEnable()
	{
		jumpDelay = 7f;
		ropeHit = true;
		jumps = 0;
		jumpCount.text = 0 + "/5";
		playtime.audioDevice.PlayOneShot(playtime.aud_Instrcutions);
	}

	private void Update()
	{
		if (jumpDelay > 0f)
		{
			jumpDelay -= Time.deltaTime;
		}
		else if (!jumpStarted)
		{
			jumpStarted = true;
			ropePosition = 1f;
			rope.SetTrigger("ActivateJumpRope");
			ropeHit = false;
		}
		if (ropePosition > 0f)
		{
			ropePosition -= Time.deltaTime;
		}
		else if (!ropeHit)
		{
			RopeHit();
		}
	}

	private void RopeHit()
	{
		ropeHit = true;
		if (cs.jumpHeight == 0f)
		{
			Fail();
		}
		else
		{
			Success();
		}
		jumpStarted = false;
	}

	private void Success()
	{
		playtime.audioDevice.PlayOneShot(playtime.aud_Numbers[jumps]);
		jumps++;
		jumpCount.text = jumps + "/5";
		jumpDelay = 0.5f;
		if (jumps >= 5)
		{
			ps.DeactivateJumpRope();
			playtime.audioDevice.PlayOneShot(playtime.aud_Congrats);
		}
	}

	private void Fail()
	{
		jumps = 0;
		jumpCount.text = jumps + "/5";
		jumpDelay = 8f;
		playtime.audioDevice.PlayOneShot(playtime.aud_Oops);
	}
}
