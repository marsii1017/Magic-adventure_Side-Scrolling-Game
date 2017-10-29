using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OOP專題_BETA
{
    public partial class Form1 : Form
    {
        Image player_1;
        Image player_bullet_1;
        Image player_bullet_2;
        Image monster_bullet_1;
        Image zone_gravity;
        Image zone_water;
        Image enemy_1;
        Image enemy_1_hit;
        Image bkground_1_1;
        Image bkground_1_2;
        Image bkground_2_1;
        Image bkground_2_2;



        public struct character
        {
            public string name;
            public bool hit;
            public int vy;
            public int vx;
            public int x;
            public int y;
            public int health;
            public int shield;
            public int mana;
            public int exp;
            public int attack;
            public int mode;
            public int time;
        }
        character[] player = new character[2];
        const int MONSTER_N = 50;
        character[] monster = new character[MONSTER_N];

        public struct bullet_type
        {
            public string name;
            public int belong;
          //  public bool active;
            public int x;
            public int y;
            public int vx;
            public int vy;
            public int attack;
            public int tag;
            public int mode;
            public bool penetrate;
        }
        const int BULLET_N=600;
        bullet_type[] bullet = new bullet_type[BULLET_N];

        const int ZONE_N = 10;
        public struct zone_type
        {
            public string name;
         //   public bool move;
            public int x;
            public int y;
            public int vx;
            public int vy;
            public int lengh;
            public int height;
        }
        zone_type[] zone = new zone_type[ZONE_N];

        void system_initial()
        {
            player_1 = new Bitmap(Properties.Resources.player_2, 50, 50);
            monster_bullet_1 = new Bitmap(Properties.Resources.bullet_002, 20, 20);
            player_bullet_1 = new Bitmap(Properties.Resources.bullet_001, 15, 15);
            player_bullet_2 = new Bitmap(Properties.Resources.bullet_003, 50, 50);
            zone_gravity = new Bitmap(Properties.Resources.zone_gravity, 300, 300);
            zone_water = new Bitmap(Properties.Resources.zone_water, 300, 300);
            enemy_1 = new Bitmap(Properties.Resources.enemy_1, 50, 50);
            enemy_1_hit = new Bitmap(Properties.Resources.enemy_1_hit, 50, 50);
            bkground_1_1 = new Bitmap(Properties.Resources.背景1_1, 400, 600);
            bkground_1_2 = new Bitmap(Properties.Resources.背景1_2, 400, 600);
            bkground_2_1 = new Bitmap(Properties.Resources.背景2_1, 400, 600);
            bkground_2_2 = new Bitmap(Properties.Resources.背景2_2, 400, 600);
        }
        void game_initial()
        {
            for (int i = 0; i < BULLET_N; i++)
            {
                bullet[i].name = "NULL";
            }
            for (int i = 0; i < ZONE_N; i++)
            {
                zone[i].name = "NULL";
            }
            for (int i = 0; i < MONSTER_N; i++)
            {
                monster[i].name = "NULL";
            }

            player[0].mode = 1;
            player[0].x = 200;
            player[0].y = 100;
            player[0].vx = 7;
            player[0].vy = 7;
            player[0].health = 100;
            player[0].mana = 100;
            player[0].exp = 0;

            button1.Enabled = false;
            button1.Visible = false;

            timer1.Enabled = true;
            timer2_count = -3;
        }
        public Form1()
        {
            InitializeComponent();

            system_initial();
            game_initial();

            
        }

        //Basic timer
        int basic_timer_time = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            system();
            basic_timer_time++;
            player_move_control();
            bkground_set();
           
                
                player_shoot();
                
                bullet_set();
                hit();
                bullet_bound_detect();
                main_monster();
           
            //    zone_generate();
                zone_effect();
           //     monster_generate();
            

                zone_set();
                zone_bound_detect();

                player_state();
                player_bound_detect();

            this.Invalidate();
        }

        //system
        void system()
        {
            if (player[0].health <= 0)
            {
                timer1.Enabled = false;
                label2.Visible = true;
                button1.Enabled = true;
                button1.Visible = true;
            }
            else
            {
                timer1.Enabled = true;
                label2.Visible = false;
                button1.Enabled = false;
                button1.Visible = false;
            }
        }

        //Draw
        void draw_character(PaintEventArgs e)
        {
            e.Graphics.DrawImage(player_1,player[0].x,player[0].y);
        }
        void draw_monster(PaintEventArgs e)
        {
            for (int i = 0; i < MONSTER_N; i++)
            {
                if (monster[i].name == "darkbat")
                {
                //    label1.Text ="monster";
                    if (monster[i].hit==false)
                        e.Graphics.DrawImage(enemy_1, monster[i].x, monster[i].y,50,50);
                    else
                        e.Graphics.DrawImage(enemy_1_hit, monster[i].x, monster[i].y, 50, 50);
                }
            }
        }
        void draw_bullet(PaintEventArgs e)
        {
            for (int i = 0; i < BULLET_N; i++)
            {
                if (bullet[i].name == "straight_bullet" && bullet[i].belong==1)
                {
                    e.Graphics.DrawImage(player_bullet_1,bullet[i].x,bullet[i].y);
                }
                else if (bullet[i].name == "straight_bullet" && bullet[i].belong == 2)
                {
                    e.Graphics.DrawImage(monster_bullet_1, bullet[i].x, bullet[i].y);
                }
                else if (bullet[i].name == "arrow" && bullet[i].belong == 1)
                {
                    e.Graphics.DrawImage(player_bullet_2, bullet[i].x, bullet[i].y);
                }
            }

        }
        void draw_zone(PaintEventArgs e)
        {
            for (int i = 0; i < ZONE_N; i++)
            {
                if (zone[i].name == "gravity")
                {
                    e.Graphics.DrawImage(zone_gravity, zone[i].x, zone[i].y, zone[i].lengh, zone[i].height);
                }
                else if (zone[i].name == "water")
                {
                    e.Graphics.DrawImage(zone_water,zone[i].x, zone[i].y, zone[i].lengh, zone[i].height);
                }
                else if (zone[i].name == "magnet_n")
                {
                    e.Graphics.DrawImage(zone_water, zone[i].x, zone[i].y, zone[i].lengh, zone[i].height);
                }
            }
        }

        int bk_1_position = 0;
        int bk_2_position = 0;
        void draw_bkground(PaintEventArgs e)
        {
            for (int i = 0; i < 2; i++)
            {
                e.Graphics.DrawImage(bkground_2_1, bk_2_position + i * 800, 0);
                e.Graphics.DrawImage(bkground_2_2, bk_2_position + i * 800+400, 0);
            }
            for (int i = 0; i < 2; i++)
            {
                e.Graphics.DrawImage(bkground_1_1, bk_1_position + i * 800, 0);
                e.Graphics.DrawImage(bkground_1_2, bk_1_position + i * 800+400, 0);
            }
           
        }
        void bkground_set()
        {
            bk_1_position -= 6;
            if (bk_1_position <= -800) bk_1_position = 0;
            bk_2_position -= 3;
            if (bk_2_position <= -800) bk_2_position = 0;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            draw_bkground(e);
            draw_character(e);
            draw_bullet(e);
            draw_monster(e);
            draw_zone(e);
            
        }

        //player shoot
        int player_shoot_timeinterval=0;
        void player_shoot()
        {
            player_shoot_timeinterval++;
            if (shoot_control[1] == true)
            {
                if (player[0].mode == 1)
                    player_bullet_initial(1);
                else if(player[0].mode == 2)
                    player_bullet_initial(2);
                else if (player[0].mode == 3)
                    player_bullet_initial(3);
            }
            if (shoot_control[2] == true)
            {
                if ((player_shoot_timeinterval) % 8 == 0&&player[0].mana>0)
                {
                    player[0].mana -= 10;
                    bullet[Bullet_I].name = "arrow";
                    bullet[Bullet_I].belong = 1;
                    bullet[Bullet_I].x = player[0].x + 50;
                    bullet[Bullet_I].y = player[0].y + 25;
                    bullet[Bullet_I].attack = 30;
                    bullet[Bullet_I].vx = 15;
                    bullet[Bullet_I].penetrate = true;
                    Bullet_I = (++Bullet_I) % BULLET_N;   
                }
            }
                
                
           // if (shoot_control[2] == true)
                
        }
        void player_bullet_initial(int mode)
        {
           
            if (mode == 1 && bullet[Bullet_I].name == "NULL")
            {
                if ((player_shoot_timeinterval) % 8 == 0)
                {
                    bullet[Bullet_I].name = "straight_bullet";
                    bullet[Bullet_I].belong = 1;
                    bullet[Bullet_I].x = player[0].x + 50;
                    bullet[Bullet_I].y = player[0].y + 25;
                    bullet[Bullet_I].attack = 10;
                    bullet[Bullet_I].vx = 10;
                    bullet[Bullet_I].vy = 0;
                    bullet[Bullet_I].penetrate = false;
                    Bullet_I = (++Bullet_I) % BULLET_N;
                }
            }
            else if (mode == 2 && bullet[Bullet_I].name == "NULL")
            {
                if ((++player_shoot_timeinterval) % 8 == 0)
                {
                    int n = 2;
                    while ((n--) != 0)
                    {
                        bullet[Bullet_I].name = "straight_bullet";
                        bullet[Bullet_I].belong = 1;
                        bullet[Bullet_I].x = player[0].x + 50 ;
                        bullet[Bullet_I].y = player[0].y + 5 + n * 20;
                        bullet[Bullet_I].attack = 10;
                        bullet[Bullet_I].vx = 10;
                        bullet[Bullet_I].penetrate = false;
                        Bullet_I = (++Bullet_I) % BULLET_N;
                    }
                }
            }
            else if (mode == 3 && bullet[Bullet_I].name == "NULL")
            {
                if ((++player_shoot_timeinterval) % 8 == 0)
                {
                    int n = 3;
                    while ((n--) != 0)
                    {
                        bullet[Bullet_I].name = "straight_bullet";
                        bullet[Bullet_I].belong = 1;
                        bullet[Bullet_I].x = player[0].x + 50;
                        bullet[Bullet_I].y = player[0].y + 5 + n * 20;
                        bullet[Bullet_I].attack = 10;
                        bullet[Bullet_I].vx = 10;
                        bullet[Bullet_I].vy = -2 + n * 2;
                        bullet[Bullet_I].penetrate = false;
                        Bullet_I = (++Bullet_I) % BULLET_N;
                    }
                }
            }
            
            
                
        }
        int player_state_timeinterval = 0;
        void player_state()
        {
            player[0].exp = player[0].exp < 100 ? player[0].exp : 100;
            panel3.Width = (int)(150 * ((float)(player[0].health) / 100));
            panel4.Width = (int)(150 * ((float)(player[0].mana) / 100));
            panel6.Width = (int)(150 * ((float)(player[0].exp) / 100));
            player[0].mode = player[0].exp / 33<3?player[0].exp / 33+1:3;

            if ((++player_state_timeinterval) % 100 == 0)
            {
                if (player[0].mana < 100) player[0].mana += 10;
            }
           // label1.Text = Convert.ToString(player[0].health);
        }
        void player_bound_detect()
        {
            if (player[0].x > 750)
            {
                player[0].x = 750;
            }
            else if (player[0].x <0)
            {
                player[0].x = 0;
            }
            else if (player[0].y > 550)
            {
                player[0].y = 550;
            }
            else if (player[0].y < 0)
            {
                player[0].y = 0;
            }
        }

        
        //Monster
        int Monster_I = 0;
        Random rnd = new Random();//rnd.Next(50, 550);
        void main_monster()
        {
            for (int i = 0; i < MONSTER_N; i++)
            {
                monster_set(i);
                monster_bound_detect(i);
                monster_state(i);
                montser_shoot(i);
                
            }
        }
        int monster_generate_timeinterval=0;
        void monster_generate()
        {
            //int n = 3;
            if((++monster_generate_timeinterval)%50==0)
            {
                int tx;
                tx=rnd.Next(50, 550);
                for (int i = 0; i < 3; i++)
                {
                    monster_initial(800 + 50 * i, tx, "darkbat");
                }
            }
        }
        void monster_initial(int x,int y,string m_name)
        {
            if (monster[Monster_I].name == "NULL")
            {
                
                if (m_name == "darkbat")
                {
                    monster[Monster_I].name = m_name;
                    monster[Monster_I].x = x;
                    monster[Monster_I].y = y;
                    monster[Monster_I].vx = -5;
                    monster[Monster_I].vy = 0;
                    monster[Monster_I].health = 50;
                    monster[Monster_I].hit = false;
                    monster[Monster_I].time = 1;
                    Monster_I = (++Monster_I) % MONSTER_N;
                }
                else
                {
                    Monster_I = (++Monster_I) % MONSTER_N;
                }
            }
        }
        void monster_set(int i)
        {
         //   for (int i = 0; i < MONSTER_N; i++)
          //  {
                if (monster[i].name == "darkbat")
                {
                    monster[i].x += monster[i].vx;
                }
          //  }
        }
        void monster_bound_detect(int i)
        {
       //     for (int i = 0; i < MONSTER_N; i++)
        //    {
                if (monster[i].x > 900 || monster[i].x < -100 || monster[i].y > 700 || monster[i].y < -100)
                {
                    monster[i].name = "NULL";
                }
         //   }
        }
        void monster_state(int i)
        {
          //  for (int i = 0; i < MONSTER_N; i++)
          //  {
                if (monster[i].name != "NULL") monster[i].time++;
                if (monster[i].health <= 0)
                {
                    if (monster[i].hit == true && monster[i].name!="NULL")
                        player[0].exp += 10;
                    monster[i].name = "NULL";
                }
               
                if (monster[i].hit == true && monster[i].time % 4 == 0)
                    monster[i].hit = false;
          //  }
        }
        void monster_bullet_initial(int i)
        {
         
         //   for (int i = 0; i < MONSTER_N; i++)
        //    {
            if (monster[i].name == "darkbat")
            {
                bullet[Bullet_I].name = "straight_bullet";
                bullet[Bullet_I].belong = 2;
                bullet[Bullet_I].x = monster[i].x;
                bullet[Bullet_I].y = monster[i].y + 20;
                bullet[Bullet_I].attack = 10;
                bullet[Bullet_I].vx = -10;
                bullet[Bullet_I].vy = 0;
                bullet[Bullet_I].penetrate = false;
                Bullet_I = (++Bullet_I) % BULLET_N;
            }
               
         //   }
        }
        void montser_shoot(int i)
        {
          //  for (int i = 0; i < MONSTER_N; i++)
          //  {
                if (monster[i].time % 100 == 0) monster_bullet_initial(i);
          //  }
        }

        //Bullet
        int Bullet_I = 0;
        void bullet_set()
        {
            for (int i = 0; i < BULLET_N; i++)
            {
                if (bullet[i].name == "straight_bullet")
                {
                    bullet[i].x += bullet[i].vx;
                    bullet[i].y += bullet[i].vy;
                }
                if (bullet[i].name == "arrow")
                {
                    bullet[i].x += bullet[i].vx;
                    bullet[i].y += bullet[i].vy;
                }
            }
        }
        void bullet_bound_detect()
        {
            for (int i = 0; i < BULLET_N; i++)
            {
                if (bullet[i].x > 800|| bullet[i].x < 0 || bullet[i].y > 600 || bullet[i].y < 0)
                {
                    delete_bullet(i);
                }
            }
        }
        void delete_bullet(int i)
        {
            bullet[i].name = "NULL";
            bullet[i].mode = 0;
            bullet[i].x = 0;
            bullet[i].y = 0;
            bullet[i].vx = 0;
            bullet[i].vy = 0;
        }

        //Zone
        int Zone_I = 0;
        void zone_generate()
        {
            int zone_mode = 2;
          //  zone_initial(zone_mode);
        }
        void zone_initial(int zone_mode,int x,int y,int lengh,int height)
        {
            if ( zone_mode == 1)
            {
                zone[Zone_I].name = "gravity";
                zone[Zone_I].x = x;
                zone[Zone_I].y = y;
                zone[Zone_I].vx = -rnd.Next(3, 5);
                zone[Zone_I].vy = 0;
                zone[Zone_I].lengh = lengh;
                zone[Zone_I].height = height;
                Zone_I = (++Zone_I) % ZONE_N;
            }
            if (zone_mode == 2)
            {
                zone[Zone_I].name = "water";
                zone[Zone_I].x = x;
                zone[Zone_I].y = y;
                zone[Zone_I].vx = -rnd.Next(3, 5);
                zone[Zone_I].vy = 0;
                zone[Zone_I].lengh = lengh;
                zone[Zone_I].height = height;
                Zone_I = (++Zone_I) % ZONE_N;
            }
            if (zone_mode == 3)
            {
                zone[Zone_I].name = "magnet_n";
                zone[Zone_I].x = x;
                zone[Zone_I].y = y;
                zone[Zone_I].vx = -2;
                zone[Zone_I].vy = 0;
                zone[Zone_I].lengh = lengh;
                zone[Zone_I].height = height;
                Zone_I = (++Zone_I) % ZONE_N;
            }
            /*
            if (zone[0].name == "NULL" && zone_mode==2)
            {
                zone[0].name = "gravity";
                zone[0].x = 800;
                zone[0].y = rnd.Next(0,300);
                zone[0].vx = -rnd.Next(3,5);
                zone[0].vy = 0;
                zone[0].lengh = rnd.Next(100, 500);
                zone[0].height = rnd.Next(100, 500);
            }
            if (zone[1].name == "NULL" && zone_mode == 2)
            {
                zone[1].name = "water";
                zone[1].x = 800;
                zone[1].y = rnd.Next(0, 300);
                zone[1].vx = -rnd.Next(3, 5);
                zone[1].vy = 0;
                zone[1].lengh = rnd.Next(100, 500);
                zone[1].height = rnd.Next(100, 500);
            }
            if (zone[2].name == "NULL" && zone_mode == 3)
            {
                zone[2].name = "black_hole";
                zone[2].x = 400;
                zone[2].y = 100;
                zone[2].lengh = 50;
                zone[2].height = 50;
            }
            */   
        }
        bool touch = false;
        bool touch2 = false;
        int gravity_timeinterval = 0;
        void zone_effect()
        {
            for (int j = 0; j < BULLET_N; j++)
            {
                for (int i = 0; i < ZONE_N; i++)
                {
                    if (bullet[j].x > zone[i].x && bullet[j].x < zone[i].x + zone[i].lengh && bullet[j].y > zone[i].y && bullet[j].y < zone[i].y + zone[i].height)
                        touch = true;
                    else
                        touch=false;
                    
                    if (player[0].x > zone[i].x && player[0].x < zone[i].x + zone[i].lengh && player[0].y > zone[i].y && player[0].y < zone[i].y + zone[i].height)
                        touch2 = true;
                    else
                        touch2 = false;
                    
                    
                    
                    if (zone[i].name == "gravity" )
                    {
                        if (bullet[j].name!="arrow")
                        {
                            if ((++gravity_timeinterval % 1 == 0) && touch)
                                bullet[j].vy += 1;
                        }
                    }
                    else if (zone[i].name == "water" && touch)
                    {

                        if (bullet[j].name != "arrow")
                        {
                            if (bullet[j].vx < 0)
                            {
                                bullet[j].vx = -1;
                                bullet[j].x -= 5;
                                bullet[j].y -= 1;
                            }
                            else
                            {
                                if (bullet[j].vy >= zone[i].y + zone[i].height)
                                    bullet[j].vy = zone[i].y + zone[i].height;
                                else
                                {
                                    bullet[j].vx -= (int)(bullet[j].vx * 0.6);//need to correct
                                    bullet[j].vy -= (int)(bullet[j].vy * 0.6);
                                    bullet[j].y -= 2;
                                    bullet[j].vx -= (int)(bullet[j].vx * 0.6);
                                    bullet[j].vy -= (int)(bullet[j].vy * 0.6);
                                    bullet[j].y += 1;

                                }
                            }
                        }                      
                    }
                    else if (zone[i].name == "magnet_n")
                    {
                        int xx = (bullet[j].x - (zone[i].x + zone[i].lengh / 2));
                        int yy = (bullet[j].y - (zone[i].y + zone[i].height / 2));
                        int rr = (int)Math.Sqrt(xx * xx + yy * yy);
                        int kx = (bullet[j].x - zone[i].x) > 0 ? 1 : -1;
                        int ky = (bullet[j].y - zone[i].y) > 0 ? 1 : -1;

                        if (Math.Sqrt(xx * xx + yy * yy) < 25)
                        {
                            delete_bullet(j);
                            xx = 1;
                            yy = 1;
                        }
                        else if (xx != 0 && yy != 0 && basic_timer_time % 5 == 0)
                        {

                            bullet[j].vx += kx * (int)((300 / rr) * 1 * (Math.Cosh(Math.Atan(yy / xx))));
                            bullet[j].vy += ky * (int)((300 / rr) * 1 * (Math.Sinh(Math.Atan(yy / xx))));
                            
                           // label1.Text = bullet[j].name;
                        }
                    }
                  //  label1.Text = Convert.ToString(Math.Tan(45*(3.1415926/180)));
                }
            }
        }
        void zone_set()
        {
            for(int i=0;i<ZONE_N;i++)
            {
                zone[i].x += zone[i].vx;
                zone[i].y += zone[i].vy;
            }
        }
        void zone_bound_detect()
        {
            for (int i = 0; i < ZONE_N; i++)
            {
                if ((zone[i].x > 800 + zone[i].lengh) || (zone[i].x < -zone[i].lengh) || (zone[i].y > 700 + zone[i].height) || (zone[i].y < -zone[i].height))
                {
                    zone[i].name = "NULL";
                }
            }
        }

        //Hit
        void hit()
        {
             int X, Y;
            for (int i = 0; i < BULLET_N; i++)
            {
                for (int j = 0; j < MONSTER_N; j++)
                {
                    X = bullet[i].x+10  - (monster[j].x+25) ;
                    Y = bullet[i].y+10  - (monster[j].y+25) ;
                    if ((bullet[i].name=="straight_bullet") && (bullet[i].belong==1) && (monster[j].name!="NULL"))
                    {
                        if (Math.Sqrt(X * X + Y * Y) < 20)
                        {
                       //     label1.Text = "hit";
                            monster[j].health -= bullet[i].attack;
                            monster[j].hit = true;

                            if (bullet[i].penetrate == false) 
                                delete_bullet(i);
                        }
                    }

                    X = bullet[i].x + 25 - (monster[j].x + 25);
                    Y = bullet[i].y + 25 - (monster[j].y + 25);
                    if ((bullet[i].name == "arrow") && (bullet[i].belong == 1) && (monster[j].name != "NULL"))
                    {
                        if (Math.Sqrt(X * X + Y * Y) < 25)
                        {
                            //     label1.Text = "hit";
                            monster[j].health -= bullet[i].attack;
                            monster[j].hit = true;

                            if (bullet[i].penetrate == false)
                                delete_bullet(i);
                        }
                    }


                    X = bullet[i].x + 10 - (player[0].x + 25);
                    Y = bullet[i].y + 10 - (player[0].y + 25);
                    if ((bullet[i].name != "NULL") && (bullet[i].belong == 2))
                    {
                        if (Math.Sqrt(X * X + Y * Y) < 25)
                        {
                            player[0].health -= bullet[i].attack;
                            if (bullet[i].penetrate == false)
                                delete_bullet(i);
                        }
                    }

                    X = player[0].x + 25 - (monster[j].x + 25);
                    Y = player[0].y + 25 - (monster[j].y + 25);
                    if ((monster[j].name != "NULL"))
                    {
                        if (Math.Sqrt(X * X + Y * Y) < 25)
                        {
                               //  label1.Text = "hit";
                               player[0].health -= 10;

                             if (bullet[i].penetrate == false)
                                  monster[j].name = "NULL";
                        }
                    }
                }
            }

        }
         
        //Control
        bool[] control = new bool[5];
        bool[] shoot_control = new bool[5];
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        { 
            if (e.KeyCode == Keys.Up) control[1] = true;
            else if (e.KeyCode == Keys.Down) control[2] = true;
            else if (e.KeyCode == Keys.Left) control[3] = true;
            else if (e.KeyCode == Keys.Right) control[4] = true;

            if (e.KeyCode == Keys.Z) shoot_control[1] = true;
            if (e.KeyCode == Keys.X) shoot_control[2] = true;
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) control[1] = false;
            else if (e.KeyCode == Keys.Down) control[2] = false;
            else if (e.KeyCode == Keys.Left) control[3] = false;
            else if (e.KeyCode == Keys.Right) control[4] = false;

            if (e.KeyCode == Keys.Z) shoot_control[1] = false;
            if (e.KeyCode == Keys.X) shoot_control[2] = false;
        }

        void player_move_control()
        {
            if (control[1] == true) player[0].y -= 10;
            else if (control[2] == true) player[0].y += player[0].vy;
            else if (control[3] == true) player[0].x -= player[0].vx;
            else if (control[4] == true) player[0].x += player[0].vx;
        }

        int timer2_count = -3;
        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2_count++;
            if (timer2_count==0) zone_initial(3, 800, 300, 50, 50);
            
            if (timer2_count >= 0 && timer2_count < 10)
            {
                int tx;
                tx = rnd.Next(50, 550);
                monster_initial(800 + 50 , tx, "darkbat");
            }
            else if (timer2_count == 10)
            {
                zone_initial(1, 800, 0, 200, 200);
                zone_initial(1, 830, 150, 200, 200);
                zone_initial(1, 860, 300, 200, 200);
                zone_initial(1, 890, 450, 200, 200);
            }
            else if (timer2_count >= 10 && timer2_count < 20)
            {
                int tx;
                tx = rnd.Next(50, 550);
                for (int i = 0; i < 3; i++)
                {
                    monster_initial(800 + 50*i, tx, "darkbat");
                }
            }
            else if (timer2_count == 20)
            {
                zone_initial(2, 800, 200, 600, 200);
            }
            else if (timer2_count >= 20 && timer2_count < 30)
            {
                for (int i = 0; i < 5; i++)
                {
                    monster_initial(800, 50 * i, "darkbat");
                    monster_initial(900, 600 - 50 * i, "darkbat");
                }
            }
            if (timer2_count >= 30 &&timer2_count%5==0)
            {
                int x, y,h,l,mode;
                y = rnd.Next(0, 600);
                for (int i = 0; i < 3; i++)
                {
                    monster_initial(800 + 50 * i, y, "darkbat");
                }
             //   for (int i = 0; i < 3; i++)
             //   {
               //     mode = rnd.Next(0,2);
                    x = rnd.Next(0, 800);
                    y = rnd.Next(0, 600);
                    h = rnd.Next(100, 200);
                    l = rnd.Next(100, 200);
                    zone_initial(1,x,y,h,l);

               //     mode = rnd.Next(0, 2);
                    x = rnd.Next(0, 800);
                    y = rnd.Next(0, 600);
                    h = rnd.Next(100, 200);
                    l = rnd.Next(100, 200);
                    zone_initial(2, x, y, h, l);

                    x = rnd.Next(0, 800);
                    y = rnd.Next(0, 600);
                  
                    zone_initial(3, x, y, 50, 50);
             //   }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            game_initial();
        }
    }
}
