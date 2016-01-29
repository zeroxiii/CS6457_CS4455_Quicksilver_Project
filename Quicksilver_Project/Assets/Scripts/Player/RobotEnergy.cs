// Team Quicksilver
// Rahmaan Lodhia
// William Gu
// Larry He
// Mitch Leff

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RobotEnergy : MonoBehaviour 
{
	public int startingEnergy;
	public int currentEnergy;
	public float flashSpeed = 5f;
	public Image damageImage;
	public GameObject energyBar;
	public int dashCost;
	public int attackCost;
	public int shootCost;
	public int shieldCost;
	public int shrinkCost;
	public int standardCost;
	public Color damageColor = new Color(1f, 0f, 0f, 0.1f);


	bool damaged;
	bool isDead;
	private GUIBarScript energyMeter;

	// Use this for initialization
	void Start () 
	{
		currentEnergy = startingEnergy;
		if (energyBar != null)
		{
			energyMeter = energyBar.GetComponent<GUIBarScript>();
			energyMeter.Value = (currentEnergy/100f);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(damaged)
		{
			damageImage.color = damageColor;
		}
		else
		{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;

		if (energyBar != null)
		{
			energyMeter.Value = (currentEnergy/100f);
		}
	}

	public void TakeDamage (int amount)
	{
		damaged = true;
		
		currentEnergy -= amount;

		if(currentEnergy <= 0 && !isDead)
		{
			Death ();
		}
	}

	public void AddEnergy (int amount)
	{
		currentEnergy += amount;
	}

	public void StartEnergyDrain()
	{
		InvokeRepeating("StandardEnergyDrain", 5f, 3f);
	}

	public void StartShieldDrain()
	{
		InvokeRepeating("ShieldEnergyDrain", 0, 2f);
	}

	public void StopShieldDrain()
	{
		CancelInvoke("ShieldEnergyDrain");
	}

	public void StandardEnergyDrain()
	{
		DecreaseEnergy(standardCost);
	}

	public void ShieldEnergyDrain()
	{
		DecreaseEnergy(shieldCost);
	}

	public void IncreaseEnergy (int amount)
	{
		currentEnergy += amount;
	}

	public void DecreaseEnergy (int amount)
	{
		currentEnergy -= amount;
	}

	public void ShrinkEnergyCost()
	{
		DecreaseEnergy(shrinkCost);
	}

	public void DashEnergyCost()
	{
		DecreaseEnergy(dashCost);
	}


	public void ShootEnergyCost()
	{
		DecreaseEnergy(shootCost);
	}

	public void AttackEnergyCost()
	{
		DecreaseEnergy(attackCost);
	}

	void Death ()
	{
		isDead = true;	
		energyMeter.Value = 0f;
		GameObject.Find("GameManager").GetComponent<GameManager>().OpenSuccessScreen();
	}

}
