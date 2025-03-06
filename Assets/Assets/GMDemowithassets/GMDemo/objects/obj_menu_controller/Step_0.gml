/// @description Insert description here
// You can write your code in this editor
//menu inputs
key_down = keyboard_check_pressed(vk_down)
key_up = keyboard_check_pressed(vk_up)
key_select = keyboard_check_pressed(vk_enter)
key_right = keyboard_check_pressed(vk_right)
key_left = keyboard_check_pressed(vk_left)
//Start menu 
if(key_down) && (obj_start_text.image_index == 1) && (obj_start_text.visible == true)
{
 obj_quit.image_index = 1
 obj_start_text.image_index = 0
 }
 if(key_up) && (obj_quit.image_index == 1) && (obj_quit.visible == true) 
 {
 obj_quit.image_index = 0
 obj_start_text.image_index = 1
 }
 if(obj_start_text.image_index == 1) && (key_select) && (obj_start_text.visible == true)
{
	alarm_set(0,10)
	obj_forest_valley.visible = true
	obj_desolate_plains_txt.visible = true
	obj_start_text.visible = false
	obj_quit.visible = false
}
 if(obj_quit.image_index == 1) && (key_select) 
{
	game_end();
}
//Level Select




if(key_select) && (obj_forest_valley.image_speed > 0)  room_goto(1)

