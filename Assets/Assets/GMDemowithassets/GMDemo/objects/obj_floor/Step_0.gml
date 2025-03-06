/// @description Insert description here
// You can write your code in this editor
if (place_meeting(x+1,y,obj_daggerWEP))
{
with(instance_place(x,y,obj_daggerWEP))
instance_destroy();
}
if (place_meeting(x+1,y,obj_dagger_blueWEP))
{
with(instance_place(x,y,obj_dagger_blueWEP))
instance_destroy();
}