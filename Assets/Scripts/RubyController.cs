using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;
    public float timeInvincible = 2.0f;

    public int maxHealth = 5;
    public static int ammo = 3;
    public static int FixedRobots = 0;
    public static int ExtraHealth = 0;

    public bool HasCrowbar = false;
    public bool HasHealthSatchel = false;
    


    public GameObject projectilePrefab;
    public ParticleSystem hitEffect;
    public ParticleSystem HealEffect;

    public int health { get { return currentHealth; } }
    int currentHealth;

    
    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;


    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    AudioSource audioSource; //Audio
    public AudioClip Walk;
    public AudioClip ThrowCog;
    public AudioClip Hit;
    public AudioClip collectedClip;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        PauseMenu.isPaused = false;
    }

    public void PlaySound(AudioClip clip) //Audio
    {
        audioSource.PlayOneShot(clip);
    }

    
    void Update()
    {
        if (!PauseMenu.isPaused)
        {


            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            Vector2 move = new Vector2(horizontal, vertical);

            if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
            {
                lookDirection.Set(move.x, move.y);
                lookDirection.Normalize();

            }

            animator.SetFloat("Look X", lookDirection.x);
            animator.SetFloat("Look Y", lookDirection.y);
            animator.SetFloat("Speed", move.magnitude);

            if (isInvincible)
            {
                invincibleTimer -= Time.deltaTime;
                if (invincibleTimer < 0)
                    isInvincible = false;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                if (ammo > 0)
                {
                    Launch();
                    PlaySound(ThrowCog);

                }

            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));


                if (hit.collider != null)
                {
                    NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                    if (character != null)
                    {


                        if (character.NPCType == 2)
                        {
                            if (FixedRobots >= character.FixRobotRequirement)
                            {
                                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                            }
                            else
                                character.DisplayDialog();

                        }



                        if (character.NPCType == 3)
                        {
                            if (FixedRobots >= character.FixRobotRequirement)
                            {
                                character.DisplayDialog();

                                if (HasHealthSatchel == false)
                                {
                                    HasHealthSatchel = true;
                                    PlaySound(collectedClip);
                                }



                            }

                        }

                        if (character.NPCType == 4)
                        {
                            if (FixedRobots >= character.FixRobotRequirement)
                            {
                                character.DisplayDialog();
                                if (HasCrowbar == false)
                                {
                                    HasCrowbar = true;
                                    PlaySound(collectedClip);
                                }

                            }
                        }

                        
                        if (character.NPCType == 7)
                        {
                            SetMaxHealth(10);
                            character.DisplayDialog();
                            if (currentHealth < maxHealth)
                            {
                                PlaySound(collectedClip);
                                ChangeHealth(maxHealth);
                                UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);

                            }
                        }

                        if (character.NPCType == 5)
                        {

                            if (ExtraHealth < 2)
                                AddExtraHealth(2);

                            if (FixedRobots >= character.FixRobotRequirement)
                            {
                                character.DisplayDialog2();
                                Debug.Log("wtf");
                                if (HasCrowbar == false)
                                    {
                                        HasCrowbar = true;
                                        PlaySound(collectedClip);
                                    }

                            }
                            else
                                character.DisplayDialog(); // can only have one else statement for this

                        }

                        if (ammo < 10)
                        {
                            AddAmmo(10 - ammo);
                            AmmoDisplay.instance.SetAmmo(ammo);
                        }
                        else
                            character.DisplayDialog();
                    }
                    





                }



            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                if (HasHealthSatchel == true && currentHealth < maxHealth && ExtraHealth > 0)
                {
                    ChangeHealth(1);
                    ExtraHealth -= 1;
                    ExtraHealthDisplay.instance.SetExtraHealth(ExtraHealth);
                    PlaySound(collectedClip);
                }
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);

        if (health == 0)
            SceneManager.LoadScene("GameOver");

    }


    public void ChangeHealth(int amount)
    {

        if (amount < 0)
        {
            if (isInvincible)
                return;
            animator.SetTrigger("Hit");
            hitEffect.Play();
            PlaySound(Hit);
            

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        else
        {
            HealEffect.Play();
        }
        

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    public void AddFixedCounter(int amount)
    {
        FixedRobots += amount;
    }


    public void AddAmmo(int amount)
    {
        ammo += amount;       
    }

    public void AddExtraHealth(int amount)
    {
        ExtraHealth += amount;
        ExtraHealthDisplay.instance.SetExtraHealth(amount);
    }

    public void SetMaxHealth(int amount)
    {
        maxHealth = amount;
        
    }
    void Launch()
    {
        if (ammo > 0)
        {

            GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(lookDirection, 300);

            animator.SetTrigger("Launch");
            ammo -= 1;
            AmmoDisplay.instance.SetAmmo(ammo);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            RaycastHit2D BreakBox = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("Breakable"));
            if (Input.GetKey(KeyCode.X))
            {
                if (BreakBox.collider != null)
                {
                    Box breakable = BreakBox.collider.GetComponent<Box>();
                    if (breakable != null)
                    {
                        if (HasCrowbar == true)
                        {
                            breakable.Break();
                        }
                    }
                }

            }
        }
        /// Allowing player to walk onto layer to change levels
        if (collision.gameObject.layer == 13)
        {
            RaycastHit2D Gateway = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("Transition"));
            
                if (Gateway.collider != null)
                {
                    LevelTransitioner Transition = Gateway.collider.GetComponent<LevelTransitioner>();
                    if (Transition != null && FixedRobots >= Transition.RequiredRobots )
                    {
                        Transition.ChangeScene();
                    }
                }
                    
            
        }
            
    }
        /// Allowing players to interact with layers and walk
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 13)
        {
            RaycastHit2D Gateway = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("Transition"));
            if (Input.GetKey(KeyCode.X))
            {
                if (Gateway.collider != null)
                {
                    LevelTransitioner Transition = Gateway.collider.GetComponent<LevelTransitioner>();
                    if (Transition != null && FixedRobots >= Transition.RequiredRobots)
                    {
                        Transition.ChangeScene();
                    }
                }

            }
        }
    }

    public void GiveCrowbar()
    {
        HasCrowbar = true;
    }

    public void GiveExtraHealthSatchel()
    {
        HasHealthSatchel = true;
    }


}