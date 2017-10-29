using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OOP專ÓM題D_BETA
{
    public partial class Form1 : Form
    {
        Image player_1;
        Image player_bullet_1;
        Image player_bullet_2;
        Image monster_bullet_1;
        Image monster_bullet_2;
        Image zone_gravity;
        Image zone_water;
        Image zone_magnet;
        Image enemy_1;
        Image enemy_1_hit;
        Image bkground_1_1;
        Image bkground_1_2;
        Image jewel_01;
        Image number001;
        Image shield;
        Image explosion;
        Image health_boost;
        Image power_boost;

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
            public int x;
            public int y;
            public int vx;
            public int vy;
            public int attack;
            public int tag;
            public int mode;
            public bool penetrate;
            public bool hit;
        }
        const int BULLET_N=600;
        bullet_type[] bullet = new bullet_type[BULLET_N];

        const int ZONE_N = 10;
        public struct zone_type
        {
            public string name;
            public int x;
            public int y;
            public int vx;
            public int vy;
            public int lengh;
            public int height;
        }
        zone_type[] zone = new zone_type[ZONE_N];

        //item
        const int ITEM_N = 20;
        public struct item_type
        {
            public string name;
            public int x;
            public int y;
            public int vx;
            public int vy;
        }
        item_type[] item = new item_type[ITEM_N];

        //label
        const int NUMBER_N=50;
        int Number_I = 0;
        struct number_type
        {
           public bool active;
           public string name;
           public int x;
           public int y;
           public int time;
        }
        number_type[] number = new number_type[NUMBER_N];
        
        public Form1()
        {
            InitializeComponent();

            system_initial();
            main();

        }

        void main()
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
            label3.Visible = label4.Visible = label5.Visible = label6.Visible = label7.Visible =  false;
            panel1.Visible = panel2.Visible = panel3.Visible = panel4.Visible = panel5.Visible = panel6.Visible = panel7.Visible = false; 
            label8.Visible = label9.Visible = true;
            button2.Enabled = false;
            button2.Visible = false;
            button1.Enabled = true;
            button1.Visible = true;
            button1.Text = "start";
            label14.Visible = false;

            player[0].x = 470;
            player[0].y = 500;
        }

        //initial
        void system_initial()
        {
            player_1 = new Bitmap(Properties.Resources.player_2, 50, 50);
            monster_bullet_1 = new Bitmap(Properties.Resources.bullet_002, 20, 20);
            monster_bullet_2 = new Bitmap(Properties.Resources.bullet004, 50, 50);
            player_bullet_1 = new Bitmap(Properties.Resources.bullet_001, 15, 15);
            player_bullet_2 = new Bitmap(Properties.Resources.bullet_003, 50, 50);
            zone_gravity = new Bitmap(Properties.Resources.zone_gravity, 300, 300);
            zone_water = new Bitmap(Properties.Resources.zone_water, 300, 300);
            zone_magnet = new Bitmap(Properties.Resources.zone_magnect, 300, 300);
            enemy_1 = new Bitmap(Properties.Resources.enemy_1, 50, 50);
            enemy_1_hit = new Bitmap(Properties.Resources.enemy_1_hit, 50, 50);
            bkground_1_1 = new Bitmap(Properties.Resources.背I景¦o, 1000, 600);
            bkground_1_2 = new Bitmap(Properties.Resources.前e景¦o, 1000, 600);
            jewel_01 = new Bitmap(Properties.Resources.jewel, 50, 50);
            number001 = new Bitmap(Properties.Resources.number001, 50, 50);
            shield = new Bitmap(Properties.Resources.player_shield, 80, 80);
            explosion = new Bitmap(Properties.Resources.player_explosion, 80, 80);
            health_boost = new Bitmap(Properties.Resources.jewel_002, 50, 50);
            power_boost = new Bitmap(Properties.Resources.jewel_003, 50, 50);
        }
        void game_initial()
        {
            label8.Visible = label9.Visible = false;
            label3.Visible = label4.Visible = label5.Visible = label6.Visible = label7.Visible = true;
            panel1.Visible = panel2.Visible = panel3.Visible = panel4.Visible = panel5.Visible = panel6.Visible = panel7.Visible = true;
            for (int i = 0; i < BULLET_N; i++)
            {
                bullet[i].name = "NULL";
                bullet[i].vx = 0;
                bullet[i].vy = 0;
            }
            for (int i = 0; i < ZONE_N; i++)
            {
                zone[i].name = "NULL";
            }
            for (int i = 0; i < MONSTER_N; i++)
            {
                monster[i].name = "NULL";
            }
            for (int i = 0; i < ITEM_N; i++)
            {
                item[i].name = "NULL";
            }
            for (int i = 0; i < NUMBER_N; i++)
            {
                number[i].active = false;
            }

            player[0].mode = 1;
            player[0].x = 200;
            player[0].y = 100;
            player[0].vx = 7;
            player[0].vy = 7;
            player[0].health = 1000;
            player[0].mana = 100;
            player[0].exp = 0;
            player[0].attack = 10;

            button1.Enabled = false;
            button1.Visible = false;

            score = 0;
            control[1] = control[2] = control[3] = control[4] = false;
            shoot_control[1] = shoot_control[2] = false;

            timer1.Enabled = true;
            timer2.Enabled = true;
            timer2_count = -3;
        }

        //Basic timer
        int basic_timer_time = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            basic_timer_time++;

            system();
            player_move_control();
            player_state();
            player_shoot();
            hit();
            main_set();
            item_set();
            item_bound_detect();
            zone_set();
            zone_bound_detect();
            player_bound_detect();
            display();
                
            this.Invalidate();
        }
        void main_set()
        {
            for (int i = 0; i < MONSTER_N; i++)
            {
                main_monster(i);
            }
            for (int j = 0; j < BULLET_N; j++)
            {
                main_bullet(j);
                zone_effect(j);
            }
        }

        //system
        void system()
        {
            if (player[0].health <= 0)
            {
                label14.Visible = true;
                label14.Text = Convert.ToString(score);
                label2.Visible = true;
                button2.Enabled = true;
                button2.Visible = true;
                for (int i = 0; i < BULLET_N; i++)
                {
                    bullet[i].name = "NULL";
                    bullet[i].vx = 0;
                    bullet[i].vy = 0;
                }
                for (int i = 0; i < ZONE_N; i++)
                {
                    zone[i].name = "NULL";
                }
                for (int i = 0; i < MONSTER_N; i++)
                {
                    monster[i].name = "NULL";
                }
                for (int i = 0; i < ITEM_N; i++)
                {
                    item[i].name = "NULL";
                }
                for (int i = 0; i < NUMBER_N; i++)
                {
                    number[i].active = false;
                }
                timer1.Enabled = false;
            }
            else
            {
                timer1.Enabled = true;
                label2.Visible = false;
                button2.Enabled = false;
                button2.Visible = false;
            }
        }
        Random rnd = new Random();//rnd.Next(50, 550);

        //Draw
        void draw_character(PaintEventArgs e)
        {
            e.Graphics.DrawImage(player_1,player[0].x,player[0].y);
            for (int i = 0; i < NUMBER_N; i++)
            {
                if (number[i].active == true) e.Graphics.DrawImage(number001, number[i].x, number[i].y);
            }
            if (player[0].hit == true)
            {
                e.Graphics.DrawImage(shield, player[0].x - 20, player[0].y - 20);
            }
            if (player[0].health <= 0)
            {
                e.Graphics.DrawImage(explosion, player[0].x, player[0].y);
            }
        }
        void draw_monster(PaintEventArgs e)
        {
            for (int i = 0; i < MONSTER_N; i++)
            {
                if (monster[i].name == "darkbat")
                {
                    if (monster[i].hit==false)
                        e.Graphics.DrawImage(enemy_1, monster[i].x, monster[i].y,50,50);
                    else
                        e.Graphics.DrawImage(enemy_1_hit, monster[i].x, monster[i].y, 50, 50);
                }
                if (monster[i].name == "defender")
                {
                    if (monster[i].hit == false)
                        e.Graphics.DrawImage(enemy_1, monster[i].x, monster[i].y, 50, 50);
                    else
                        e.Graphics.DrawImage(enemy_1_hit, monster[i].x, monster[i].y, 50, 50);
                }
                if (monster[i].name == "boss")
                {
                    if (monster[i].hit == false)
                        e.Graphics.DrawImage(enemy_1, monster[i].x, monster[i].y, 50, 50);
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
                else if (bullet[i].name == "arrow" && bullet[i].belong == 2)
                {
                    e.Graphics.DrawImage(monster_bullet_2, bullet[i].x, bullet[i].y);
                }
                else if (bullet[i].name == "target_m" && bullet[i].belong == 2)
                {
                    e.Graphics.DrawImage(monster_bullet_1, bullet[i].x, bullet[i].y);
                }
                else if (bullet[i].name == "bouuce" && bullet[i].belong == 2)
                {
                    e.Graphics.DrawImage(monster_bullet_1, bullet[i].x, bullet[i].y);
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
                    e.Graphics.DrawImage(zone_magnet, zone[i].x, zone[i].y, zone[i].lengh, zone[i].height);
                }
            }
        }
        void draw_item(PaintEventArgs e)
        {
            for (int i = 0; i < ITEM_N; i++)
            {
                if (item[i].name == "score_boost")
                {
                    e.Graphics.DrawImage(jewel_01, item[i].x, item[i].y);
                }
                else if (item[i].name == "health_boost")
                {
                    e.Graphics.DrawImage(health_boost, item[i].x, item[i].y);
                }
                else if (item[i].name == "power_boost")
                {
                    e.Graphics.DrawImage(power_boost, item[i].x, item[i].y);
                }
            }
        }
      
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            draw_character(e);
            draw_bullet(e);
            draw_monster(e);
            draw_zone(e);
            draw_item(e);
        }

        //label
        void display()
        {
            for (int i = 0; i < NUMBER_N; i++)
            {
                if(basic_timer_time%10==0) number[i].time++;
                if (number[i].active==true&&number[i].time > 3)
                {
                    number[i].active = false;
                }
            }
        }

        //player shoot
        int player_shoot_timeinterval=0;
        void player_shoot()
        {
            label1.Text = Convert.ToString(shoot_control[1]);
            player_shoot_timeinterval++;
            if (shoot_control[1] == true)
            {
                if (player[0].mode == 1)
                    player_bullet_initial(1);
                if(player[0].mode == 2)
                    player_bullet_initial(2);
                if(player[0].mode == 3)
                    player_bullet_initial(3);
            }
            if (shoot_control[2] == true)
            {
                if ((player_shoot_timeinterval) % 8 == 0&&player[0].mana>0)
                {
                    player[0].mana -= 10;
                    bullet[Bullet_I].name = "arrow";
                    bullet[Bullet_I].belong = 1;
                    bullet[Bullet_I].x = player[0].x + 40;
                    bullet[Bullet_I].y = player[0].y + 8+rnd.Next(-15,15);
                    bullet[Bullet_I].attack = 30;
                    bullet[Bullet_I].vx = 15;
                    bullet[Bullet_I].vy = 0;
                    bullet[Bullet_I].penetrate = true;
                    Bullet_I = (++Bullet_I) % BULLET_N;   
                }
            }               
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
                    bullet[Bullet_I].attack = player[0].attack;
                    bullet[Bullet_I].vx = 10;
                    bullet[Bullet_I].vy = 0;
                    bullet[Bullet_I].penetrate = false;
                    Bullet_I = (++Bullet_I) % BULLET_N;
                }
            }
            else if (mode == 2 && bullet[Bullet_I].name == "NULL")
            {
                if ((player_shoot_timeinterval) % 8 == 0)
                {
                    int n = 2;
                    while ((n--) != 0)
                    {
                        bullet[Bullet_I].name = "straight_bullet";
                        bullet[Bullet_I].belong = 1;
                        bullet[Bullet_I].x = player[0].x + 50 ;
                        bullet[Bullet_I].y = player[0].y + 5 + n * 20;
                        bullet[Bullet_I].attack = player[0].attack;
                        bullet[Bullet_I].vx = 10;
                        bullet[Bullet_I].vy = 0;
                        bullet[Bullet_I].penetrate = false;
                        Bullet_I = (++Bullet_I) % BULLET_N;
                    }
                }
            }
            else if (mode == 3 && bullet[Bullet_I].name == "NULL")
            {
                if ((player_shoot_timeinterval) % 8 == 0)
                {
                    int n = 3;
                    while ((n--) != 0)
                    {
                        bullet[Bullet_I].name = "straight_bullet";
                        bullet[Bullet_I].belong = 1;
                        bullet[Bullet_I].x = player[0].x + 50;
                        bullet[Bullet_I].y = player[0].y + 5 + n * 20;
                        bullet[Bullet_I].attack = player[0].attack;
                        bullet[Bullet_I].vx = 10;
                        bullet[Bullet_I].vy = -2 + n * 2;
                        bullet[Bullet_I].penetrate = false;
                        Bullet_I = (++Bullet_I) % BULLET_N;
                    }
                }
            }
            
            
                
        }
        int player_state_timeinterval = 0;
        int score = 0;
        void player_state()
        {
        
            panel3.Width = (int)(150 * ((float)(player[0].health) / 1000));
            panel4.Width = (int)(150 * ((float)(player[0].mana) / 100));
            panel6.Width = (int)(150 * ((float)(player[0].exp) / 100));
            label11.Text = Convert.ToString(player[0].mode);
            label13.Text = Convert.ToString(player[0].attack);

            if (player[0].health > 1000)
            {
                player[0].health = 1000;
            }
                
            if (player[0].mode < 3)
            {
                if (player[0].exp / 100 == 1)
                {
                    player[0].mode++;
                    player[0].exp /= 100;
                }
                player[0].exp = player[0].exp % 100;
            }
            else 
            {
                player[0].mode = 3;
                player[0].exp = 100;
            }
            label7.Text = Convert.ToString(score);

            if (player[0].hit==true&&basic_timer_time % 10 == 0) player[0].time++;
            if (player[0].time > 3)
            {
                player[0].hit = false;
                
            }

            if ((++player_state_timeinterval) % 4 == 0)
            {
                if (player[0].mana < 100) player[0].mana += 1;
            }
        }
        void player_bound_detect()
        {
            if (player[0].x +50> 985)
            {
                player[0].x = 935;
            }
            else if (player[0].x <0)
            {
                player[0].x = 0;
            }
            else if (player[0].y+50> 563)
            {
                player[0].y = 513;
            }
            else if (player[0].y < 0)
            {
                player[0].y = 0;
            }
        }

        //Monster
        int Monster_I = 0;
        void main_monster(int i)
        {
            monster_set(i);
            monster_bound_detect(i);
            monster_state(i);
            montser_shoot(i);   
        }
        void monster_initial(int x,int y,string m_name,int vx,int vy)
        {
            if (monster[Monster_I].name == "NULL")
            {
                
                if (m_name == "darkbat")
                {
                    monster[Monster_I].name = m_name;
                    monster[Monster_I].x = x;
                    monster[Monster_I].y = y;
                    monster[Monster_I].vx = vx;
                    monster[Monster_I].vy = vy;
                    monster[Monster_I].health = 50;
                    monster[Monster_I].hit = false;
                    monster[Monster_I].time = 1;
                    Monster_I = (++Monster_I) % MONSTER_N;
                }
                else if (m_name == "defender")
                {
                    monster[Monster_I].name = m_name;
                    monster[Monster_I].x = x;
                    monster[Monster_I].y = y;
                    monster[Monster_I].vx = vx;
                    monster[Monster_I].vy = vy;
                    monster[Monster_I].health = 50;
                    monster[Monster_I].hit = false;
                    monster[Monster_I].time = 1;
                    Monster_I = (++Monster_I) % MONSTER_N;
                }
                else if (m_name == "boss")
                {
                    monster[Monster_I].name = m_name;
                    monster[Monster_I].x = x;
                    monster[Monster_I].y = y;
                    monster[Monster_I].vx = vx;
                    monster[Monster_I].vy = vy;
                    monster[Monster_I].health = 1000;
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
            if (monster[i].name == "darkbat")
            {
                monster[i].x += monster[i].vx;
                monster[i].y += monster[i].vy;
            }
            if (monster[i].name == "defender")
            {
                monster[i].x += monster[i].vx;
                monster[i].y += monster[i].vy;
            }
            if (monster[i].name == "boss")
            {
                if (basic_timer_time % 400 == 0)
                {
                    zone_initial(3, monster[i].x, monster[i].y, -2, 0, 50, 50);
                    zone_initial(2, monster[i].x, monster[i].y-150, -2, 0, 80, 300);
                }

                if (monster[i].health > 250)
                {
                    if ((monster[i].y - player[0].y) > 2) monster[i].y -= monster[i].vy;
                    else if ((monster[i].y - player[0].y) < -2) monster[i].y += monster[i].vy;
                    if ((monster[i].x - player[0].x) < 598) monster[i].x += monster[i].vx;
                    else if ((monster[i].x - player[0].x) > 602) monster[i].x -= monster[i].vx;
                }
                if (monster[i].health <= 250)
                {
                    if ((monster[i].y - player[0].y) > 2) monster[i].y -= monster[i].vy;
                    else if ((monster[i].y - player[0].y) < -2) monster[i].y += monster[i].vy;
                    if ((monster[i].x - player[0].x) < 498) monster[i].x += monster[i].vx;
                    else if ((monster[i].x - player[0].x) > 502) monster[i].x -= monster[i].vx;
                }
             

        }
        }
        void monster_bound_detect(int i)
        {
            if (monster[i].x > 1200 || monster[i].x < -200 || monster[i].y > 600 || monster[i].y < -200)
            {
                monster[i].name = "NULL";
            }
        }
        void monster_state(int i)
        {
            int p;
                if (monster[i].name != "NULL") monster[i].time++;
                if (monster[i].health <= 0)
                {
                    if (monster[i].name == "boss") score += 10000;
                    if (monster[i].hit == true && monster[i].name != "NULL")
                    {
                        player[0].exp += 5;
                        p = rnd.Next(1, 100);
                        if (p <= 10) item_initial(monster[i].x, monster[i].y, 1);
                        else if (p <= 40 && p > 10) item_initial(monster[i].x, monster[i].y, 2);
                        else if (p > 40) item_initial(monster[i].x, monster[i].y, 3);
                        
                    }
                    monster[i].name = "NULL";
                }
               
                if (monster[i].hit == true && monster[i].time % 4 == 0)
                    monster[i].hit = false;
        }
        void monster_bullet_initial(int i)
        {
            if (monster[i].name == "darkbat")
            {
                double kx, ky,xx,yy,rr;
                xx = player[0].x - (monster[i].x + 25);
                yy = player[0].y - (monster[i].y + 25);
                rr = Math.Sqrt(xx * xx + yy * yy);
                kx = xx / rr; 
                ky = yy / rr;
                bullet[Bullet_I].name = "target_m";
                bullet[Bullet_I].belong = 2;
                bullet[Bullet_I].x = monster[i].x;
                bullet[Bullet_I].y = monster[i].y + 20;
                bullet[Bullet_I].attack = 10;
                bullet[Bullet_I].vx = (int)(kx*5);
                bullet[Bullet_I].vy = (int)(ky*5);
                bullet[Bullet_I].penetrate = false;
                Bullet_I = (++Bullet_I) % BULLET_N;
            }
            if (monster[i].name == "defender")
            {
                bullet[Bullet_I].name = "straight_bullet";
                bullet[Bullet_I].belong = 2;
                bullet[Bullet_I].x = monster[i].x;
                bullet[Bullet_I].y = monster[i].y + 20;
                bullet[Bullet_I].attack = 50;
                bullet[Bullet_I].vx = -5;
                bullet[Bullet_I].vy = 0;
                bullet[Bullet_I].penetrate = false;
                Bullet_I = (++Bullet_I) % BULLET_N;
            }
            if (monster[i].name == "boss")
            {
                double kx, ky, xx, yy, rr;
                xx = player[0].x - (monster[i].x + 25);
                yy = player[0].y - (monster[i].y + 25);
                rr = Math.Sqrt(xx * xx + yy * yy);
                kx = xx / rr;
                ky = yy / rr;
                bullet[Bullet_I].name = "target_m";
                bullet[Bullet_I].belong = 2;
                bullet[Bullet_I].x = monster[i].x;
                bullet[Bullet_I].y = monster[i].y + 20;
                bullet[Bullet_I].attack = 100;
                bullet[Bullet_I].vx = (int)(kx * 9);
                bullet[Bullet_I].vy = (int)(ky * 9);
                bullet[Bullet_I].penetrate = false;
                Bullet_I = (++Bullet_I) % BULLET_N;

                bullet[Bullet_I].name = "arrow";
                bullet[Bullet_I].belong = 2;
                bullet[Bullet_I].x = monster[i].x;
                bullet[Bullet_I].y = monster[i].y;
                bullet[Bullet_I].attack =100;
                bullet[Bullet_I].vx = -20;
                bullet[Bullet_I].vy = 0;
                bullet[Bullet_I].penetrate = false;
                Bullet_I = (++Bullet_I) % BULLET_N;   
            }
            
        }
        void montser_shoot(int i)
        {
            if (monster[i].time % 100 >= 0&&monster[i].time % 100 <= 40)
            {
                if (monster[i].time % 20==0)
                     monster_bullet_initial(i);
            }
        }

        //Bullet
        int Bullet_I = 0;
        void main_bullet(int i)
        {
            if (bullet[i].name != "NULL")
            {
                bullet_set(i);
                bullet_bound_detect(i);
            }   
        }
        void bullet_set(int i)
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
            if (bullet[i].name == "target_m")
            {
                bullet[i].x += bullet[i].vx;
                bullet[i].y += bullet[i].vy;
            } 
        }
        void bullet_bound_detect(int i)
        {
           
                if (bullet[i].x > 1000|| bullet[i].x < 0 || bullet[i].y > 600 || bullet[i].y < 0)
                {
                    delete_bullet(i);
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
            bullet[i].hit = false;
            bullet[i].penetrate = false;
        }

        //Zone
        int Zone_I = 0;
        void zone_initial(int zone_mode,int x,int y,int vx,int vy,int lengh,int height)
        {
            if ( zone_mode == 1)
            {
                zone[Zone_I].name = "gravity";
                zone[Zone_I].x = x;
                zone[Zone_I].y = y;
                zone[Zone_I].vx = vx;
                zone[Zone_I].vy = vy;
                zone[Zone_I].lengh = lengh;
                zone[Zone_I].height = height;
                Zone_I = (++Zone_I) % ZONE_N;
            }
            if (zone_mode == 2)
            {
                zone[Zone_I].name = "water";
                zone[Zone_I].x = x;
                zone[Zone_I].y = y;
                zone[Zone_I].vx = vx;
                zone[Zone_I].vy = vy;
                zone[Zone_I].lengh = lengh;
                zone[Zone_I].height = height;
                Zone_I = (++Zone_I) % ZONE_N;
            }
            if (zone_mode == 3)
            {
                zone[Zone_I].name = "magnet_n";
                zone[Zone_I].x = x;
                zone[Zone_I].y = y;
                zone[Zone_I].vx = vx;
                zone[Zone_I].vy = vy;
                zone[Zone_I].lengh = lengh;
                zone[Zone_I].height = height;
                Zone_I = (++Zone_I) % ZONE_N;
            }
           
        }
        bool touch = false;
        int gravity_timeinterval = 0;
        void zone_effect(int j)
        {
            for (int i = 0; i < ZONE_N; i++)
            {
                if (bullet[j].x > zone[i].x && bullet[j].x < zone[i].x + zone[i].lengh && bullet[j].y > zone[i].y && bullet[j].y < zone[i].y + zone[i].height)
                    touch = true;
                else
                    touch = false;


                if (zone[i].name == "gravity")
                {
                    if (bullet[j].name != "arrow")
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
                                bullet[j].vx -= (int)(bullet[j].vx * 0.6);
                                bullet[j].vy -= (int)(bullet[j].vy * 0.6);
                                bullet[j].y -= 2;
                                bullet[j].vx -= (int)(bullet[j].vx * 0.6);
                                bullet[j].vy -= (int)(bullet[j].vy * 0.6);
                                bullet[j].y += 1;
                            }
                        }
                    }
                }
                else if (zone[i].name == "magnet_n" && bullet[j].name != "arrow")
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
                    }
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
                if ((zone[i].x > 1000 + zone[i].lengh) || (zone[i].x < -zone[i].lengh) || (zone[i].y > 700 + zone[i].height) || (zone[i].y < -zone[i].height))
                {
                    zone[i].name = "NULL";
                }
            }
        }

        //item
        int Item_I=0;
        void item_initial(int x,int y,int type)
        {
            if (type == 1)
            {
                item[Item_I].name = "power_boost";
                item[Item_I].x = x;
                item[Item_I].y = y;
                item[Item_I].vx = 1;
                item[Item_I].vy = 0;
                Item_I = (++Item_I) % ITEM_N;
            }
            else if (type == 2)
            {
                item[Item_I].name = "health_boost";
                item[Item_I].x = x;
                item[Item_I].y = y;
                item[Item_I].vx = 1;
                item[Item_I].vy = 0;
                Item_I = (++Item_I) % ITEM_N;
            }
            else if (type == 3)
            {
                item[Item_I].name = "score_boost";
                item[Item_I].x = x;
                item[Item_I].y = y;
                item[Item_I].vx = -2;
                item[Item_I].vy = 0;
                Item_I = (++Item_I) % ITEM_N;
            }
        }
        void item_set()
        {
            for (int i = 0; i < ITEM_N; i++)
            {
                if (item[i].name != "NULL")
                {
                    item[i].x += item[i].vx;
                    item[i].y += item[i].vy;
                }
            }
        }
        void item_bound_detect()
        {
            for (int i = 0; i < ITEM_N; i++)
            {
                if (item[i].x > 1050 || item[i].x < -50 || item[i].y > 550 || item[i].y < 0)
                {
                    item[i].name = "NULL";
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
                            monster[j].health -= bullet[i].attack;
                            monster[j].hit = true;
                            bullet[i].hit = true;
                            score += 3;

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
                            monster[j].health -= bullet[i].attack;
                            monster[j].hit = true;
                            bullet[i].hit = true;
                            score += 5;

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
                            bullet[i].hit = true;
                            player[0].time = 0;
                            player[0].hit = true;
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

                            if (monster[j].name == "boss")
                            {
                                if (basic_timer_time % 1000 == 0)
                                    player[0].health -= 50;
                            }
                            else
                            {
                                monster[j].name = "NULL";
                                player[0].health -= 10;
                                player[0].hit = true;
                                player[0].time = 0;
                            }
                               
                        }
                    }
                }
                for (int s = 0; s < ITEM_N; s++)
                {
                    X = player[0].x + 25 - (item[s].x + 15);
                    Y = player[0].y + 25 - (item[s].y + 15);
                    if (Math.Sqrt(X * X + Y * Y) < 25)
                    {
                        if (item[s].name == "score_boost")
                        {
                            score += 100;
                            if (number[Number_I].active == false)
                            {
                                number[Number_I].active = true;
                                number[Number_I].x = player[0].x;
                                number[Number_I].y = player[0].y;
                                number[Number_I].time = 0;
                                Number_I = (++Number_I) % NUMBER_N;
                            }
                        }
                        else if (item[s].name == "health_boost")
                        {
                            player[0].health += 50;
                        }
                        else if (item[s].name == "power_boost")
                        {
                            player[0].attack += 5;
                        }
                        
                        item[s].name = "NULL";
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

        int timer2_count = -5;
        private void timer2_Tick(object sender, EventArgs e)
        {   
            timer2_count++;
           
            
            if (timer2_count == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    monster_initial(700 + 39 * i, -50 - 39 * i, "defender", -4, 4);
                    monster_initial(800 + 39 * i, -50 - 39 * i, "defender", -4, 4);
                    monster_initial(900 + 39 * i, -50 - 39 * i, "defender", -4, 4);
                }
            }
            if (timer2_count == 3)
            {
                zone_initial(3, 100, 200, 0, 2, 50, 50);
                zone_initial(3, 100, 599, 0, -2, 50, 50);
                for (int i = 0; i < 5; i++)
                {
                    monster_initial(200, 0 - 39 * i, "defender", 0, 2);
                }

            }
            
            if (timer2_count==5) zone_initial(3, 800, 300,-3,0, 50, 50);
            
            if (timer2_count >= 5 && timer2_count < 15)
            {
                int tx;
                tx = rnd.Next(50, 550);
                monster_initial(800 + 50 , tx, "darkbat",-3,0);
            }
            else if (timer2_count == 15)
            {
                zone_initial(1, 1000, 0,-3,0 ,600, 600);
                zone_initial(1, 0, 0, 3,0,600, 600);
            }
            else if (timer2_count >= 15 && timer2_count < 25)
            {
                int tx;
                tx = rnd.Next(50, 550);
                for (int i = 0; i < 3; i++)
                {
                    monster_initial(800 + 50*i, tx, "darkbat",-2,0);
                }
            }
            else if (timer2_count == 25)
            {
                zone_initial(2, 800, 200,-3,0 ,600, 600);
            }
            else if (timer2_count >= 25 && timer2_count < 35 && timer2_count % 2 == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    monster_initial(1000, 50 * i, "darkbat",-2,0);
                    monster_initial(1199, 600 - 50 * i, "darkbat",-2,0);
                }
            }
            if (timer2_count >= 35 &&timer2_count <60&&timer2_count%3==0)
            {
                int x, y,h,l,mode;
  
                for (int i = 0; i < 3; i++)
                {
                    y = rnd.Next(0, 600);
                    monster_initial(800 + 50 * i, y, "darkbat",-2,0);
                    y = rnd.Next(0, 600);
                    monster_initial(800 + 50 * i, y, "defender", -2, 0);
                }
                mode = rnd.Next(1, 3);
                y = rnd.Next(0, 600);
                h = l = rnd.Next(200, 600);
                zone_initial(mode, 0, y, 2, 0, h, l);

                mode = rnd.Next(1, 3);
                y = rnd.Next(0, 600);
                h = l = rnd.Next(200, 600);
                zone_initial(mode, 1000, y, -2, 0, h, l);

                zone_initial(3, 1000, 0, -2, 1, 50, 50);
            }
            if (timer2_count == 70) monster_initial(1000, 300, "boss", 5, 5);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            game_initial();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            main();
        }
    }
}

