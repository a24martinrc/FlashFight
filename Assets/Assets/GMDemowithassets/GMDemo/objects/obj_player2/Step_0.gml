/// @description Insert description here
// You can write your code in this editor
///Player movement
key_right = keyboard_check(ord("D"));
key_left = -keyboard_check(ord("A"));
key_jump = keyboard_check_pressed(ord("W"));
key_jump_held = keyboard_check(ord("W"));
key_dash = keyboard_check(ord("S")) 
key_duck = keyboard_check(ord("S")) 
key_throw = keyboard_check_pressed(ord("E")) || keyboard_check_pressed(ord("Q"));
//Return
move = key_right + key_left;
hsp = move * moving;
if (vsp == 0)
{
 falling = false
 grounded = true
 }
if(vsp < 10) vsp += grav;
if(place_meeting(x,y+1,obj_floor))
{
vsp = key_jump * -jumpheight
}
slide = key_dash * move
slide_left = -key_dash * move
if(slide == 1) && (in_air == 1)
{
moving = 24
grav = 5
 }
if(slide_left == 1) && (in_air == true)
{
moving = 24
grav = 5
}
if(vsp < 0) &&  (!key_jump_held)  vsp = max(vsp,-jumpheight/3)
//horizontal collision
if(place_meeting(x+(hsp),y,obj_floor))
    {
		while(!place_meeting(x+sign(hsp),y,obj_floor))
        {
            x+=sign(hsp);
        }
		moving = 10
		grav = 1.5
    hsp = 0
}
//vertical collision
if(place_meeting(x,y+(vsp),obj_floor))
{
    while(!place_meeting(x,y+sign(vsp),obj_floor))
    {
    y+=sign(vsp);
    }
	moving = 10
	grav = 1.5
vsp = 0
}
if(move!=0) image_xscale = move;
if(move!=0 && grounded = true) sprite_index = spr_player2_running;else sprite_index = Player_2
if(vsp < 0) 
{
sprite_index = spr_player2_jump;
falling = true;
}
if(vsp > 0) 
{
sprite_index = spr_player2_fall;
in_air = true;
}
if( slide||slide_left == 1) && (in_air == 1)
{
 not_sliding = false
 sprite_index = spr_player2_slide
 image_speed = 0
}
else
{
not_sliding = true
image_speed = 1
}
if(ducking && key_duck) 
{
sprite_index = spr_player2_duck;
hsp =0
}
if(vsp = 0) ducking = true; else ducking = false
if(vsp <=0) in_air = false
if(place_meeting(x,y,obj_dagger_blue))
{
 with(instance_place(x,y,obj_dagger_blue))
 {
 instance_destroy();
 }
picked_up = true
}

if(place_meeting(x,y,obj_daggerWEP)) && (invinsible == false)
{
alarm_set(1,8)
player_hit = true
	with(instance_place(x,y,obj_daggerWEP))
	{
		instance_destroy();
	}
global.lifer -= 3
}
if(player_hit == true) 
{
hsp = 0
vsp = 0
sprite_index = Player2_hit
invinsible = true
alarm_set(2,8)
}
if !(sprite_index == Player2_hit)
{
player_hit = false
}
if (picked_up == true) && (key_throw) && (not_sliding) && !(key_duck && ducking)
{
		if instance_create_depth(obj_player2.x,obj_player2.y+10,0,obj_dagger_blueWEP) 
		{   
 obj_dagger_blueWEP.image_xscale = obj_player2.image_xscale	
}
}
if (obj_player2.image_xscale = -1)
	{				
with instance_place(x,y,obj_dagger_blueWEP)
{	
speed = -25
}
}

if (obj_player2.image_xscale = 1) 
	{
with instance_place(x,y,obj_dagger_blueWEP)
{
speed = 25
}		
}		

x+=hsp;
y+=vsp;