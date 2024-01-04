using UnityEngine;
using UnityEngine.UI;

public class MathGameScript : MonoBehaviour
{
	public GameControllerScript gc;

	public BaldiScript baldiScript;

	public GameObject mathGame;

	public RawImage[] results = new RawImage[3];

	public Texture correct;

	public Texture incorrect;

	public InputField playerAnswer;

	public Text questionText;

	public Text questionText2;

	public Text questionText3;

	public Animator baldiFeed;

	public Transform baldiFeedTransform;

	public AudioClip bal_plus;

	public AudioClip bal_minus;

	public AudioClip bal_times;

	public AudioClip bal_divided;

	public AudioClip bal_equals;

	public AudioClip bal_howto;

	public AudioClip bal_intro;

	public AudioClip bal_screech;

	public AudioClip[] bal_numbers = new AudioClip[10];

	public AudioClip[] bal_praises = new AudioClip[5];

	public AudioClip[] bal_problems = new AudioClip[3];

	private int endDelay;

	private int problem;

	private int audioInQueue;

	private float num1;

	private float num2;

	private float num3;

	private int sign;

	private float solution;

	private bool questionInProgress;

	private bool impossibleMode;

	private AudioClip[] audioQueue = new AudioClip[20];

	private AudioSource baldiAudio;

	private void Start()
	{
		baldiAudio = GetComponent<AudioSource>();
		gc.ActivateLearningGame();
		QueueAudio(bal_intro);
		QueueAudio(bal_howto);
		NewProblem();
		if (gc.spoopMode)
		{
			baldiFeedTransform.position = new Vector3(-1000f, -1000f, 0f);
		}
	}

	private void Update()
	{
		if (!baldiAudio.isPlaying)
		{
			if ((audioInQueue > 0) & !gc.spoopMode)
			{
				PlayQueue();
			}
			baldiFeed.SetBool("talking", false);
		}
		else
		{
			baldiFeed.SetBool("talking", true);
		}
		if ((Input.GetKeyDown("return") || Input.GetKeyDown("enter")) & questionInProgress)
		{
			questionInProgress = false;
			CheckAnswer();
		}
		if (problem > 3)
		{
			endDelay--;
			if (endDelay <= 0)
			{
				ExitGame();
			}
		}
	}

	private void NewProblem()
	{
		playerAnswer.text = string.Empty;
		problem++;
		if (problem <= 3)
		{
			QueueAudio(bal_problems[problem - 1]);
			if (problem <= 2 || gc.notebooks <= 1)
			{
				num1 = Mathf.RoundToInt(Random.Range(0f, 9f));
				num2 = Mathf.RoundToInt(Random.Range(0f, 9f));
				sign = Mathf.RoundToInt(Random.Range(0f, 1f));
				QueueAudio(bal_numbers[Mathf.RoundToInt(num1)]);
				if (sign == 0)
				{
					solution = num1 + num2;
					questionText.text = "SOLVE MATH Q" + problem + ": \n \n" + num1 + "+" + num2 + "=";
					QueueAudio(bal_plus);
				}
				else if (sign == 1)
				{
					solution = num1 - num2;
					questionText.text = "SOLVE MATH Q" + problem + ": \n \n" + num1 + "-" + num2 + "=";
					QueueAudio(bal_minus);
				}
				QueueAudio(bal_numbers[Mathf.RoundToInt(num2)]);
				QueueAudio(bal_equals);
			}
			else
			{
				impossibleMode = true;
				num1 = Random.Range(1f, 9999f);
				num2 = Random.Range(1f, 9999f);
				num3 = Random.Range(1f, 9999f);
				sign = Mathf.RoundToInt(Random.Range(0, 1));
				QueueAudio(bal_screech);
				if (sign == 0)
				{
					questionText.text = "SOLVE MATH Q" + problem + ": \n" + num1 + "+(" + num2 + "X" + num3 + "=";
					QueueAudio(bal_plus);
					QueueAudio(bal_screech);
					QueueAudio(bal_times);
					QueueAudio(bal_screech);
				}
				else if (sign == 1)
				{
					questionText.text = "SOLVE MATH Q" + problem + ": \n (" + num1 + "/" + num2 + ")+" + num3 + "=";
					QueueAudio(bal_divided);
					QueueAudio(bal_screech);
					QueueAudio(bal_plus);
					QueueAudio(bal_screech);
				}
				num1 = Random.Range(1f, 9999f);
				num2 = Random.Range(1f, 9999f);
				num3 = Random.Range(1f, 9999f);
				sign = Mathf.RoundToInt(Random.Range(0, 1));
				if (sign == 0)
				{
					questionText2.text = "SOLVE MATH Q" + problem + ": \n" + num1 + "+(" + num2 + "X" + num3 + "=";
				}
				else if (sign == 1)
				{
					questionText2.text = "SOLVE MATH Q" + problem + ": \n (" + num1 + "/" + num2 + ")+" + num3 + "=";
				}
				num1 = Random.Range(1f, 9999f);
				num2 = Random.Range(1f, 9999f);
				num3 = Random.Range(1f, 9999f);
				sign = Mathf.RoundToInt(Random.Range(0, 1));
				if (sign == 0)
				{
					questionText3.text = "SOLVE MATH Q" + problem + ": \n" + num1 + "+(" + num2 + "X" + num3 + "=";
				}
				else if (sign == 1)
				{
					questionText3.text = "SOLVE MATH Q" + problem + ": \n (" + num1 + "/" + num2 + ")+" + num3 + "=";
				}
				QueueAudio(bal_equals);
			}
			playerAnswer.ActivateInputField();
			questionInProgress = true;
		}
		else
		{
			endDelay = 300;
			questionText.text = "WOW! YOU EXIST!";
		}
	}

	private void CheckAnswer()
	{
		if ((playerAnswer.text == solution.ToString()) & !impossibleMode)
		{
			results[problem - 1].texture = correct;
			baldiAudio.Stop();
			ClearAudioQueue();
			QueueAudio(bal_praises[Random.Range(0, 4)]);
			NewProblem();
			return;
		}
		results[problem - 1].texture = incorrect;
		if (!gc.spoopMode)
		{
			baldiFeed.SetTrigger("angry");
			gc.ActivateSpoopMode();
		}
		baldiScript.GetAngry(problem);
		ClearAudioQueue();
		baldiAudio.Stop();
		NewProblem();
	}

	private void QueueAudio(AudioClip sound)
	{
		audioQueue[audioInQueue] = sound;
		audioInQueue++;
	}

	private void PlayQueue()
	{
		baldiAudio.PlayOneShot(audioQueue[0]);
		UnqueueAudio();
	}

	private void UnqueueAudio()
	{
		for (int i = 1; i < audioInQueue; i++)
		{
			audioQueue[i - 1] = audioQueue[i];
		}
		audioInQueue--;
	}

	private void ClearAudioQueue()
	{
		audioInQueue = 0;
	}

	private void ExitGame()
	{
		gc.DeactivateLearningGame(mathGame);
	}
}
