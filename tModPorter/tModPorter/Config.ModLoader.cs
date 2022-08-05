﻿using static tModPorter.Rewriters.RenameRewriter;
using static tModPorter.Rewriters.InvokeRewriter;
using static tModPorter.Rewriters.MemberUseRewriter;
using static tModPorter.Rewriters.HookRewriter;
using static tModPorter.Rewriters.AddComment;
using System;
using System.IO;

namespace tModPorter;

public static partial class Config
{
	private static void AddModLoaderRefactors() {
		RenameInstanceField("Terraria.ModLoader.ModType",		from: "mod",			to: "Mod");
		RenameInstanceField("Terraria.ModLoader.ModItem",		from: "item",			to: "Item");
		RenameInstanceField("Terraria.ModLoader.EquipTexture",	from: "item",			to: "Item");
		RenameInstanceField("Terraria.ModLoader.ModNPC",		from: "npc",			to: "NPC");
		RenameInstanceField("Terraria.ModLoader.ModPlayer",		from: "player",			to: "Player");
		RenameInstanceField("Terraria.ModLoader.ModProjectile",	from: "projectile",		to: "Projectile");
		RenameInstanceField("Terraria.ModLoader.ModMount",		from: "mountData",		to: "MountData");

		RenameInstanceField("Terraria.Item",			from: "modItem",		to: "ModItem");
		RenameInstanceField("Terraria.NPC",				from: "modNPC",			to: "ModNPC");
		RenameInstanceField("Terraria.Projectile",		from: "modProjectile",	to: "ModProjectile");
		RenameInstanceField("Terraria.Mount.MountData",	from: "modMountData",	to: "ModMount");
		RenameInstanceField("Terraria.Gore",			from: "modGore",		to: "ModGore");

		RenameType(from: "Terraria.ModLoader.ModMountData", to: "Terraria.ModLoader.ModMount");
		RenameType(from: "Terraria.ModLoader.ModWorld",		to: "Terraria.ModLoader.ModSystem");
		RenameType(from: "Terraria.ModLoader.ModHotKey",	to: "Terraria.ModLoader.ModKeybind");

		RenameInstanceField("Terraria.ModLoader.TooltipLine",	from: "text",			to: "Text");
		RenameInstanceField("Terraria.ModLoader.TooltipLine",	from: "mod",			to: "Mod");
		RenameInstanceField("Terraria.ModLoader.TooltipLine",	from: "isModifier",		to: "IsModifier");
		RenameInstanceField("Terraria.ModLoader.TooltipLine",	from: "isModifierBad",	to: "IsModifierBad");
		RenameInstanceField("Terraria.ModLoader.TooltipLine",	from: "overrideColor",	to: "OverrideColor");

		RenameInstanceField("Terraria.ModLoader.ModProjectile", from: "aiType",								  to: "AIType");
		RenameInstanceField("Terraria.ModLoader.ModProjectile", from: "cooldownSlot",						  to: "CooldownSlot");
		RenameInstanceField("Terraria.ModLoader.ModProjectile", from: "drawOffsetX",				 		  to: "DrawOffsetX");
		RenameInstanceField("Terraria.ModLoader.ModProjectile", from: "drawOriginOffsetY",					  to: "DrawOriginOffsetY");
		RenameInstanceField("Terraria.ModLoader.ModProjectile", from: "drawOriginOffsetX",					  to: "DrawOriginOffsetX");
		RenameInstanceField("Terraria.ModLoader.ModProjectile", from: "drawHeldProjInFrontOfHeldItemAndArms", to: "DrawHeldProjInFrontOfHeldItemAndArms");

		RenameInstanceField("Terraria.ModLoader.ModNPC", from: "aiType",		to: "AIType");
		RenameInstanceField("Terraria.ModLoader.ModNPC", from: "animationType", to: "AnimationType");
		RenameInstanceField("Terraria.ModLoader.ModNPC", from: "music",			to: "Music");
		RenameInstanceField("Terraria.ModLoader.ModNPC", from: "musicPriority", to: "SceneEffectPriority");
		RenameInstanceField("Terraria.ModLoader.ModNPC", from: "drawOffsetY",	to: "DrawOffsetY");
		RenameInstanceField("Terraria.ModLoader.ModNPC", from: "banner",		to: "Banner");
		RenameInstanceField("Terraria.ModLoader.ModNPC", from: "bannerItem",	to: "BannerItem");

		RenameInstanceField("Terraria.ModLoader.ModGore", from: "updateType",	to: "UpdateType");
		RenameInstanceField("Terraria.ModLoader.ModDust", from: "updateType",	to: "UpdateType");

		RefactorInstanceMethodCall("Terraria.ModLoader.BuffLoader", "CanBeCleared",			Removed("Use !BuffID.Sets.NurseCannotRemoveDebuff instead"));
		RefactorInstanceMember("Terraria.ModLoader.ModBuff",		"canBeCleared",			Removed("Use BuffID.Sets.NurseCannotRemoveDebuff instead, and invert the logic"));
		RefactorInstanceMember("Terraria.ModLoader.ModBuff",		"longerExpertDebuff",	Removed("Use BuffID.Sets.LongerExpertDebuff instead"));
		RefactorInstanceMember("Terraria.ModLoader.EquipTexture",	"mod",					Removed(""));
		RefactorInstanceMember("Terraria.ModLoader.ModNPC",			"bossBag",				Removed("Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type))"));
		RefactorInstanceMember("Terraria.ModLoader.Mod",			"Properties",			Removed("Instead, assign the properties directly (ContentAutoloadingEnabled, GoreAutoloadingEnabled, MusicAutoloadingEnabled, and BackgroundAutoloadingEnabled)"));
		RefactorInstanceMember("Terraria.ModLoader.ModBlockType",	"soundStyle",			Removed("Integrate into HitSound"));
		RefactorInstanceMember("Terraria.ModLoader.ModTile",		"sapling",				Removed("Use TileID.Sets.TreeSapling and TileID.Sets.CommonSapling instead"));
		RefactorInstanceMember("Terraria.ModLoader.ModTile",		"torch",				Removed("Use TileID.Sets.Torch instead"));
		RefactorInstanceMember("Terraria.ModLoader.ModTile",		"bed",					Removed("Use TileID.Sets.CanBeSleptIn instead"));
		RefactorInstanceMember("Terraria.ModLoader.ModTile",		"dresser",				Removed("Use ContainerName.SetDefault() and TileID.Sets.BasicDresser instead"));
		RefactorInstanceMember("Terraria.ModLoader.ModTile",		"chest",				Removed("Use ContainerName.SetDefault() and TileID.Sets.BasicChest instead"));
		RefactorInstanceMember("Terraria.ModLoader.ModTile",		"disableSmartInteract",	Removed("Use TileID.Sets.DisableSmartInteract instead"));
		RefactorInstanceMember("Terraria.ModLoader.ModTile",		"disableSmartCursor",	Removed("Use TileID.Sets.DisableSmartCursor instead"));
		RefactorInstanceMethodCall("Terraria.ModLoader.ModTile",	"SetModTree",			Removed("Assign GrowsOnTileId to this tile type in ModTree.SetStaticDefaults instead"));
		RefactorInstanceMethodCall("Terraria.ModLoader.ModTile",	"SetModCactus",			Removed("Assign GrowsOnTileId to this tile type in ModCactus.SetStaticDefaults instead"));
		RefactorInstanceMethodCall("Terraria.ModLoader.ModTile",	"SetModPalmTree",		Removed("Assign GrowsOnTileId to this tile type in ModPalmTree.SetStaticDefaults instead"));

		// TODO, assignment rewriter
		// RefactorInstanceMember("Terraria.ModLoader.ModBlockType", "HitSound", Comment("Suggestion: Use a SoundStyle here"));

		RenameMethod("Terraria.ModLoader.ModItem",		from: "NetRecieve",			to: "NetReceive");
		RenameMethod("Terraria.ModLoader.ModItem",		from: "NewPreReforge",		to: "PreReforge");
		RenameMethod("Terraria.ModLoader.GlobalItem",	from: "NewPreReforge",		to: "PreReforge");
		RenameMethod("Terraria.ModLoader.ModItem",		from: "GetWeaponCrit",		to: "ModifyWeaponCrit");
		RenameMethod("Terraria.ModLoader.GlobalItem",	from: "GetWeaponCrit",		to: "ModifyWeaponCrit");
		RenameMethod("Terraria.ModLoader.ModPlayer",	from: "GetWeaponCrit",		to: "ModifyWeaponCrit");
		RenameMethod("Terraria.ModLoader.ModItem",		from: "GetWeaponKnockback",	to: "ModifyWeaponKnockback");
		RenameMethod("Terraria.ModLoader.GlobalItem",	from: "GetWeaponKnockback",	to: "ModifyWeaponKnockback");
		RenameMethod("Terraria.ModLoader.ModPlayer",	from: "GetWeaponKnockback",	to: "ModifyWeaponKnockback");
		RenameMethod("Terraria.ModLoader.ModItem",		from: "ConsumeAmmo",		to: "CanConsumeAmmo");
		RenameMethod("Terraria.ModLoader.GlobalItem",	from: "ConsumeAmmo",		to: "CanConsumeAmmo");
		RenameMethod("Terraria.ModLoader.ModPlayer",	from: "ConsumeAmmo",		to: "CanConsumeAmmo");
		RenameMethod("Terraria.ModLoader.ModNPC",		from: "NPCLoot",			to: "OnKill");
		RenameMethod("Terraria.ModLoader.GlobalNPC",	from: "NPCLoot",			to: "OnKill");
		RenameMethod("Terraria.ModLoader.ModNPC",		from: "PreNPCLoot",			to: "PreKill");
		RenameMethod("Terraria.ModLoader.GlobalNPC",	from: "PreNPCLoot",			to: "PreKill");
		RenameMethod("Terraria.ModLoader.ModNPC",		from: "SpecialNPCLoot",		to: "SpecialOnKill");
		RenameMethod("Terraria.ModLoader.GlobalNPC",	from: "SpecialNPCLoot",		to: "SpecialOnKill");
		RenameMethod("Terraria.ModLoader.ModNPC",		from: "TownNPCName",		to: "SetNPCNameList");
		RenameMethod("Terraria.ModLoader.ModTile",		from: "NewRightClick",		to: "RightClick");
		RenameMethod("Terraria.ModLoader.ModTile",		from: "Dangersense",		to: "IsTileDangerous");
		RenameMethod("Terraria.ModLoader.GlobalTile",	from: "Dangersense",		to: "IsTileDangerous");
		RenameMethod("Terraria.ModLoader.ModTileEntity",from: "ValidTile",			to: "IsTileValidForEntity");
		RenameMethod("Terraria.ModLoader.EquipTexture", from: "UpdateVanity",		to: "FrameEffects");
		RenameMethod("Terraria.ModLoader.ModPlayer",	from: "SetupStartInventory",to: "AddStartingItems");
		RenameMethod("Terraria.ModLoader.ModPrefix",	from: "AutoDefaults",		to: "AutoStaticDefaults");
		RenameMethod("Terraria.ModLoader.ModType",		from: "Autoload",			to: "IsLoadingEnabled").FollowBy(AddCommentToOverride("Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead"));
		RenameMethod("Terraria.ModLoader.ModTree",		from: "GrowthFXGore",		to: "TreeLeaf");
		RenameMethod("Terraria.ModLoader.ModPalmTree",	from: "GrowthFXGore",		to: "TreeLeaf");

		ChangeHookSignature("Terraria.ModLoader.ModItem",			"HoldStyle");
		ChangeHookSignature("Terraria.ModLoader.GlobalItem",		"HoldStyle");
		ChangeHookSignature("Terraria.ModLoader.ModItem",			"UseStyle");
		ChangeHookSignature("Terraria.ModLoader.GlobalItem",		"UseStyle");
		ChangeHookSignature("Terraria.ModLoader.ModItem",			"UseItem", comment: "Suggestion: Return null instead of false");
		ChangeHookSignature("Terraria.ModLoader.GlobalItem",		"UseItem", comment: "Suggestion: Return null instead of false");
		ChangeHookSignature("Terraria.ModLoader.ModItem",			"ModifyWeaponKnockback");
		ChangeHookSignature("Terraria.ModLoader.GlobalItem",		"ModifyWeaponKnockback");
		ChangeHookSignature("Terraria.ModLoader.ModPlayer",			"ModifyWeaponKnockback");
		ChangeHookSignature("Terraria.ModLoader.ModItem",			"ModifyWeaponCrit");
		ChangeHookSignature("Terraria.ModLoader.GlobalItem",		"ModifyWeaponCrit");
		ChangeHookSignature("Terraria.ModLoader.ModPlayer",			"ModifyWeaponCrit");
		ChangeHookSignature("Terraria.ModLoader.ModItem",			"ModifyWeaponDamage");
		ChangeHookSignature("Terraria.ModLoader.GlobalItem",		"ModifyWeaponDamage");
		ChangeHookSignature("Terraria.ModLoader.ModPlayer",			"ModifyWeaponDamage");
		ChangeHookSignature("Terraria.ModLoader.ModItem",			"Shoot");
		ChangeHookSignature("Terraria.ModLoader.GlobalItem",		"Shoot");
		ChangeHookSignature("Terraria.ModLoader.ModPlayer",			"Shoot");
		ChangeHookSignature("Terraria.ModLoader.ModItem",			"PickAmmo");
		ChangeHookSignature("Terraria.ModLoader.GlobalItem",		"PickAmmo");
		ChangeHookSignature("Terraria.ModLoader.ModItem",			"CanConsumeAmmo");
		ChangeHookSignature("Terraria.ModLoader.GlobalItem",		"CanConsumeAmmo");
		ChangeHookSignature("Terraria.ModLoader.ModItem",			"OnConsumeAmmo");
		ChangeHookSignature("Terraria.ModLoader.GlobalItem",		"OnConsumeAmmo");
		ChangeHookSignature("Terraria.ModLoader.ModNPC",			"PreDraw");
		ChangeHookSignature("Terraria.ModLoader.GlobalNPC",			"PreDraw");
		ChangeHookSignature("Terraria.ModLoader.ModNPC",			"PostDraw");
		ChangeHookSignature("Terraria.ModLoader.GlobalNPC",			"PostDraw");
		ChangeHookSignature("Terraria.ModLoader.ModProjectile",		"PreDrawExtras");
		ChangeHookSignature("Terraria.ModLoader.GlobalProjectile",	"PreDrawExtras");
		ChangeHookSignature("Terraria.ModLoader.ModProjectile",		"PreDraw");
		ChangeHookSignature("Terraria.ModLoader.GlobalProjectile",	"PreDraw");
		ChangeHookSignature("Terraria.ModLoader.ModProjectile",		"PostDraw");
		ChangeHookSignature("Terraria.ModLoader.GlobalProjectile",	"PostDraw");
		ChangeHookSignature("Terraria.ModLoader.ModProjectile",		"DrawBehind");
		ChangeHookSignature("Terraria.ModLoader.GlobalProjectile",	"DrawBehind");
		ChangeHookSignature("Terraria.ModLoader.ModProjectile",		"CanDamage", comment: "Suggestion: Return null instead of true");
		ChangeHookSignature("Terraria.ModLoader.GlobalProjectile",	"CanDamage", comment: "Suggestion: Return null instead of true");
		ChangeHookSignature("Terraria.ModLoader.ModProjectile",		"TileCollideStyle");
		ChangeHookSignature("Terraria.ModLoader.GlobalProjectile",	"TileCollideStyle");
		ChangeHookSignature("Terraria.ModLoader.ModPlayer",			"PreHurt");
		ChangeHookSignature("Terraria.ModLoader.ModPlayer",			"Hurt");
		ChangeHookSignature("Terraria.ModLoader.ModPlayer",			"PostHurt");
		ChangeHookSignature("Terraria.ModLoader.ModPlayer",			"CatchFish");
		ChangeHookSignature("Terraria.ModLoader.ModPlayer",			"AddStartingItems", comment: "Suggestion: Return an Item array to add to the players starting items. Use ModifyStartingInventory for modifying them if needed");
		ChangeHookSignature("Terraria.ModLoader.ModTile",			"SetDrawPositions");
		ChangeHookSignature("Terraria.ModLoader.ModTile",			"HasSmartInteract");
		ChangeHookSignature("Terraria.ModLoader.ModTile",			"DrawEffects");
		ChangeHookSignature("Terraria.ModLoader.GlobalTile",		"DrawEffects");
		ChangeHookSignature("Terraria.ModLoader.GlobalTile",		"IsTileDangerous", comment: "Suggestion: Return null instead of false");
		ChangeHookSignature("Terraria.ModLoader.GlobalTile",		"PlaceInWorld");
		ChangeHookSignature("Terraria.ModLoader.ModNPC",			"SetNPCNameList", comment: "Suggestion: Return a list of names");
		ChangeHookSignature("Terraria.ModLoader.ModMount",			"JumpHeight");
		ChangeHookSignature("Terraria.ModLoader.ModMount",			"JumpSpeed");
		ChangeHookSignature("Terraria.ModLoader.ModType",			"IsLoadingEnabled");
		ChangeHookSignature("Terraria.ModLoader.ModType",			"CloneNewInstances"); // public -> protected

		HookRemoved("Terraria.ModLoader.EquipTexture",	"DrawHead",		"After registering this as EquipType.Head, use ArmorIDs.Head.Sets.DrawHead[slot] = false if you returned false");
		HookRemoved("Terraria.ModLoader.ModItem",		"DrawHead",		"In SetStaticDefaults, use ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false if you returned false");
		HookRemoved("Terraria.ModLoader.GlobalItem",	"DrawHead",		"In SetStaticDefaults, use ArmorIDs.Head.Sets.DrawHead[head] = false if you returned false");
		HookRemoved("Terraria.ModLoader.EquipTexture",	"DrawBody",		"After registering this as EquipType.Body, use ArmorIDs.Body.Sets.HidesTopSkin[slot] = true if you returned false");
		HookRemoved("Terraria.ModLoader.ModItem",		"DrawBody",		"In SetStaticDefaults, use ArmorIDs.Body.Sets.HidesTopSkin[Item.bodySlot] = true if you returned false");
		HookRemoved("Terraria.ModLoader.GlobalItem",	"DrawBody",		"In SetStaticDefaults, use ArmorIDs.Body.Sets.HidesTopSkin[body] = true if you returned false");
		HookRemoved("Terraria.ModLoader.EquipTexture",	"DrawLegs",		"After registering this as EquipType.Legs or Shoes, use ArmorIDs.Legs.Sets.HidesBottomSkin[slot] = true if you returned false for EquipType.Legs, and ArmorIDs.Shoe.Sets.OverridesLegs[slot] = true if you returned false for EquipType.Shoes");
		HookRemoved("Terraria.ModLoader.ModItem",		"DrawLegs",		"In SetStaticDefaults, use ArmorIDs.Legs.Sets.HidesBottomSkin[Item.legSlot] = true if you returned false for an accessory of EquipType.Legs, and ArmorIDs.Shoe.Sets.OverridesLegs[Item.shoeSlot] = true if you returned false for an accessory of EquipType.Shoes");
		HookRemoved("Terraria.ModLoader.GlobalItem",	"DrawLegs",		"In SetStaticDefaults, use ArmorIDs.Legs.Sets.HidesBottomSkin[legs] = true, and ArmorIDs.Shoe.Sets.OverridesLegs[shoes] = true");
		HookRemoved("Terraria.ModLoader.EquipTexture",	"DrawHands",	"After registering this as EquipType.Body, use ArmorIDs.Body.Sets.HidesHands[slot] = false if you had drawHands set to true. If you had drawArms set to true, you don't need to do anything");
		HookRemoved("Terraria.ModLoader.ModItem",		"DrawHands",	"In SetStaticDefaults, use ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false if you had drawHands set to true. If you had drawArms set to true, you don't need to do anything");
		HookRemoved("Terraria.ModLoader.GlobalItem",	"DrawHands",	"In SetStaticDefaults, use ArmorIDs.Body.Sets.HidesHands[body] = false if you had drawHands set to true. If you had drawArms set to true, you don't need to do anything");
		HookRemoved("Terraria.ModLoader.EquipTexture",	"DrawHair",		"After registering this as EquipType.Head, use ArmorIDs.Head.Sets.DrawFullHair[slot] = true if you had drawHair set to true, and ArmorIDs.Head.Sets.DrawHatHair[slot] = true if you had drawAltHair set to true");
		HookRemoved("Terraria.ModLoader.ModItem",		"DrawHair",		"In SetStaticDefaults, use ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true if you had drawHair set to true, and ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true if you had drawAltHair set to true");
		HookRemoved("Terraria.ModLoader.GlobalItem",	"DrawHair",		"In SetStaticDefaults, use ArmorIDs.Head.Sets.DrawFullHair[head] = true if you had drawHair set to true, and ArmorIDs.Head.Sets.DrawHatHair[head] = true if you had drawAltHair set to true");

		HookRemoved("Terraria.ModLoader.ModItem",		"IgnoreDamageModifiers","If you returned true, consider leaving Item.DamageType as DamageClass.Default, or make a custom DamageClass which returns StatInheritanceData.None in GetModifierInheritance");
		HookRemoved("Terraria.ModLoader.ModItem",		"OnlyShootOnSwing",		"If you returned true, set Item.useTime to a multiple of Item.useAnimation");
		HookRemoved("Terraria.ModLoader.ModGore",		"DrawBehind",			"Use GoreID.Sets.DrawBehind[Type] in SetStaticDefaults");
		HookRemoved("Terraria.ModLoader.ModTile",		"SaplingGrowthType",	"Use ModTree.SaplingGrowthType");
		HookRemoved("Terraria.ModLoader.GlobalRecipe",	"RecipeAvailable",		"Use Recipe.AddCondition");
		HookRemoved("Terraria.ModLoader.GlobalRecipe",	"OnCraft",				"Use Recipe.AddOnCraftCallback or GlobalItem.OnCreate");
		HookRemoved("Terraria.ModLoader.GlobalRecipe",	"ConsumeItem",			"Use Recipe.AddConsumeItemCallback");

		HookRemoved("Terraria.ModLoader.ModSurfaceBackgroundStyle",		"ChooseBgStyle", "Create a ModBiome (or ModSceneEffect) class and override SurfaceBackgroundStyle property to return this object through Mod/ModContent.Find, then move this code into IsBiomeActive (or IsSceneEffectActive)");
		HookRemoved("Terraria.ModLoader.ModUndergroundBackgroundStyle",	"ChooseBgStyle", "Create a ModBiome (or ModSceneEffect) class and override UndergroundBackgroundStyle property to return this object through Mod/ModContent.Find, then move this code into IsBiomeActive (or IsSceneEffectActive)");
		HookRemoved("Terraria.ModLoader.ModPlayer", "SetMapBackgroundImage", "Create a ModBiome (or ModSceneEffect) class and override MapBackground property to return this object through Mod/ModContent.Find, then move this code into IsBiomeActive (or IsSceneEffectActive)");

		HookRemoved("Terraria.ModLoader.Mod","HotKeyPressed",				"Use ModPlayer.ProcessTriggers");
		HookRemoved("Terraria.ModLoader.Mod", "UpdateMusic",				"Use ModSceneEffect.Music and .Priority, aswell as ModSceneEffect.IsSceneEffectActive");
		HookRemoved("Terraria.ModLoader.Mod", "ModifyTransformMatrix",		"Use ModSystem.ModifyTransformMatrix");
		HookRemoved("Terraria.ModLoader.Mod", "UpdateUI",					"Use ModSystem.UpdateUI");
		HookRemoved("Terraria.ModLoader.Mod", "PreUpdateEntities",			"Use ModSystem.PreUpdateEntities");
		HookRemoved("Terraria.ModLoader.Mod", "MidUpdateDustTime",			"Use ModSystem.PostUpdateDusts or ModSystem.PreUpdateTime");
		HookRemoved("Terraria.ModLoader.Mod", "MidUpdateGoreProjectile",	"Use ModSystem.PostUpdateGores or ModSystem.PreUpdateProjectiles");
		HookRemoved("Terraria.ModLoader.Mod", "MidUpdateInvasionNet",		"Use ModSystem.PostUpdateInvasions");
		HookRemoved("Terraria.ModLoader.Mod", "MidUpdateItemDust",			"Use ModSystem.PostUpdateItems or ModSystem.PreUpdateDusts");
		HookRemoved("Terraria.ModLoader.Mod", "MidUpdateNPCGore",			"Use ModSystem.PostUpdateNPCs or ModSystem.PreUpdateGores");
		HookRemoved("Terraria.ModLoader.Mod", "MidUpdatePlayerNPC",			"Use ModSystem.PostUpdatePlayers or ModSystem.PreUpdateNPCs");
		HookRemoved("Terraria.ModLoader.Mod", "MidUpdateProjectileItem",	"Use ModSystem.PostUpdateProjectiles or ModSystem.PreUpdateItems");
		HookRemoved("Terraria.ModLoader.Mod", "MidUpdateTimeWorld",			"Use ModSystem.PostUpdateTime");
		HookRemoved("Terraria.ModLoader.Mod", "PostUpdateEverything",		"Use ModSystem.PostUpdateEverything");
		HookRemoved("Terraria.ModLoader.Mod", "ModifyInterfaceLayers",		"Use ModSystem.ModifyInterfaceLayers");
		HookRemoved("Terraria.ModLoader.Mod", "ModifySunLightColor",		"Use ModSystem.ModifySunLightColor");
		HookRemoved("Terraria.ModLoader.Mod", "ModifyLightingBrightness",	"Use ModSystem.ModifyLightingBrightness");
		HookRemoved("Terraria.ModLoader.Mod", "PostDrawInterface",			"Use ModSystem.PostDrawInterface");
		HookRemoved("Terraria.ModLoader.Mod", "PostDrawFullscreenMap",		"Use ModSystem.PostDrawFullscreenMap or a ModMapLayer");
		HookRemoved("Terraria.ModLoader.Mod", "PostUpdateInput",			"Use ModSystem.PostUpdateInput");
		HookRemoved("Terraria.ModLoader.Mod", "PreSaveAndQuit",				"Use ModSystem.PreSaveAndQuit");
		HookRemoved("Terraria.ModLoader.Mod", "AddRecipeGroups",			"Use ModSystem.AddRecipeGroups");
		HookRemoved("Terraria.ModLoader.Mod", "AddRecipes",					"Use ModSystem.AddRecipes");
		HookRemoved("Terraria.ModLoader.Mod", "PostAddRecipes",				"Use ModSystem.PostAddRecipes");

		RenameMethod("Terraria.ModLoader.ModItem",		from: "Load",		to: "LoadData");
		RenameMethod("Terraria.ModLoader.ModItem",		from: "Save",		to: "SaveData");
		RenameMethod("Terraria.ModLoader.GlobalItem",	from: "Load",		to: "LoadData");
		RenameMethod("Terraria.ModLoader.GlobalItem",	from: "Save",		to: "SaveData");
		RenameMethod("Terraria.ModLoader.ModPlayer",	from: "Load",		to: "LoadData");
		RenameMethod("Terraria.ModLoader.ModPlayer",	from: "Save",		to: "SaveData");
		RenameMethod("Terraria.ModLoader.ModTileEntity",from: "Load",		to: "LoadData");
		RenameMethod("Terraria.ModLoader.ModTileEntity",from: "Save",		to: "SaveData");
		RenameMethod("Terraria.ModLoader.ModSystem",	from: "Load",		to: "LoadWorldData");
		RenameMethod("Terraria.ModLoader.ModSystem",	from: "Save",		to: "SaveWorldData");
		RenameMethod("Terraria.ModLoader.ModSystem",	from: "Initialize", to: "OnWorldLoad").FollowBy(AddCommentToOverride("Suggestion: Also override OnWorldUnload, and mirror your worldgen-sensitive data initialization in PreWorldGen"));
		RenameMethod("Terraria.ModLoader.ModSystem",	from: "PreUpdate",	to: "PreUpdateWorld");
		RenameMethod("Terraria.ModLoader.ModSystem",	from: "PostUpdate", to: "PostUpdateWorld");
		ChangeHookSignature("Terraria.ModLoader.ModItem",		"CanEquipAccessory",comment: "Suggestion: Consider using new hook CanAccessoryBeEquippedWith");
		ChangeHookSignature("Terraria.ModLoader.GlobalItem",	"CanEquipAccessory",comment: "Suggestion: Consider using new hook CanAccessoryBeEquippedWith");
		ChangeHookSignature("Terraria.ModLoader.ModItem",		"SaveData",			comment: "Suggestion: Edit tag parameter instead of returning new TagCompound");
		ChangeHookSignature("Terraria.ModLoader.GlobalItem",	"SaveData",			comment: "Suggestion: Edit tag parameter instead of returning new TagCompound");
		ChangeHookSignature("Terraria.ModLoader.ModPlayer",		"SaveData",			comment: "Suggestion: Edit tag parameter instead of returning new TagCompound");
		ChangeHookSignature("Terraria.ModLoader.ModTileEntity", "SaveData",			comment: "Suggestion: Edit tag parameter instead of returning new TagCompound");
		ChangeHookSignature("Terraria.ModLoader.ModSystem",		"SaveWorldData",	comment: "Suggestion: Edit tag parameter instead of returning new TagCompound");
		ChangeHookSignature("Terraria.ModLoader.ModSystem",		"TileCountsAvailable");
		ChangeHookSignature("Terraria.ModLoader.ModItem",		"Clone");
		ChangeHookSignature("Terraria.ModLoader.ModGore",		"OnSpawn");

		RenameMethod("Terraria.ModLoader.GlobalTile",	from: "SetDefaults", to: "SetStaticDefaults");
		RenameMethod("Terraria.ModLoader.GlobalWall",	from: "SetDefaults", to: "SetStaticDefaults");
		RenameMethod("Terraria.ModLoader.InfoDisplay",	from: "SetDefaults", to: "SetStaticDefaults");
		RenameMethod("Terraria.ModLoader.ModBlockType",	from: "SetDefaults", to: "SetStaticDefaults");
		RenameMethod("Terraria.ModLoader.ModBuff",		from: "SetDefaults", to: "SetStaticDefaults");
		RenameMethod("Terraria.ModLoader.ModDust",		from: "SetDefaults", to: "SetStaticDefaults");
		RenameMethod("Terraria.ModLoader.ModMount",		from: "SetDefaults", to: "SetStaticDefaults");
		RenameMethod("Terraria.ModLoader.ModPrefix",	from: "SetDefaults", to: "SetStaticDefaults");

		RenameInstanceField("Terraria.ModLoader.ModBlockType",		from: "drop",		to: "ItemDrop");
		RenameInstanceField("Terraria.ModLoader.ModBlockType",		from: "dustType",	to: "DustType");
		RenameInstanceField("Terraria.ModLoader.ModBlockType",		from: "soundType",	to: "HitSound");

		RenameInstanceField("Terraria.ModLoader.ModTile", from: "dresserDrop",			to: "DresserDrop");
		RenameInstanceField("Terraria.ModLoader.ModTile", from: "chestDrop",			to: "ChestDrop");
		RenameInstanceField("Terraria.ModLoader.ModTile", from: "closeDoorID",			to: "CloseDoorID");
		RenameInstanceField("Terraria.ModLoader.ModTile", from: "openDoorID",			to: "OpenDoorID");
		RenameInstanceField("Terraria.ModLoader.ModTile", from: "minPick",				to: "MinPick");
		RenameInstanceField("Terraria.ModLoader.ModTile", from: "mineResist",			to: "MineResist");
		RenameInstanceField("Terraria.ModLoader.ModTile", from: "animationFrameHeight",	to: "AnimationFrameHeight");
		RenameInstanceField("Terraria.ModLoader.ModTile", from: "adjTiles",				to: "AdjTiles");

		RenameInstanceField("Terraria.ModLoader.ModWaterStyle",		from: "Type",		to: "Slot");
		RenameInstanceField("Terraria.ModLoader.ModWaterfallStyle", from: "Type",		to: "Slot");

		RenameInstanceField("Terraria.ModLoader.NPCSpawnInfo", from: "desertCave",			to: "DesertCave");
		RenameInstanceField("Terraria.ModLoader.NPCSpawnInfo", from: "granite",				to: "Granite");
		RenameInstanceField("Terraria.ModLoader.NPCSpawnInfo", from: "invasion",			to: "Invasion");
		RenameInstanceField("Terraria.ModLoader.NPCSpawnInfo", from: "lihzahrd",			to: "Lihzahrd");
		RenameInstanceField("Terraria.ModLoader.NPCSpawnInfo", from: "marble",				to: "Marble");
		RenameInstanceField("Terraria.ModLoader.NPCSpawnInfo", from: "planteraDefeated",	to: "PlanteraDefeated");
		RenameInstanceField("Terraria.ModLoader.NPCSpawnInfo", from: "player",				to: "Player");
		RenameInstanceField("Terraria.ModLoader.NPCSpawnInfo", from: "playerFloorX",		to: "PlayerFloorX");
		RenameInstanceField("Terraria.ModLoader.NPCSpawnInfo", from: "playerFloorY",		to: "PlayerFloorY");
		RenameInstanceField("Terraria.ModLoader.NPCSpawnInfo", from: "playerInTown",		to: "PlayerInTown");
		RenameInstanceField("Terraria.ModLoader.NPCSpawnInfo", from: "playerSafe",			to: "PlayerSafe");
		RenameInstanceField("Terraria.ModLoader.NPCSpawnInfo", from: "safeRangeX",			to: "SafeRangeX");
		RenameInstanceField("Terraria.ModLoader.NPCSpawnInfo", from: "sky",					to: "Sky");
		RenameInstanceField("Terraria.ModLoader.NPCSpawnInfo", from: "spawnTileType",		to: "SpawnTileType");
		RenameInstanceField("Terraria.ModLoader.NPCSpawnInfo", from: "spawnTileX",			to: "SpawnTileX");
		RenameInstanceField("Terraria.ModLoader.NPCSpawnInfo", from: "spawnTileY",			to: "SpawnTileY");
		RenameInstanceField("Terraria.ModLoader.NPCSpawnInfo", from: "spiderCave",			to: "SpiderCave");
		RenameInstanceField("Terraria.ModLoader.NPCSpawnInfo", from: "water",				to: "Water");

		RefactorInstanceMethodCall("Terraria.ModLoader.Mod", "BuffType",		ToFindTypeCall("Terraria.ModLoader.ModBuff"));
		RefactorInstanceMethodCall("Terraria.ModLoader.Mod", "DustType",		ToFindTypeCall("Terraria.ModLoader.ModDust"));
		RefactorInstanceMethodCall("Terraria.ModLoader.Mod", "ItemType",		ToFindTypeCall("Terraria.ModLoader.ModItem"));
		RefactorInstanceMethodCall("Terraria.ModLoader.Mod", "MountType",		ToFindTypeCall("Terraria.ModLoader.ModMount"));
		RefactorInstanceMethodCall("Terraria.ModLoader.Mod", "NPCType",			ToFindTypeCall("Terraria.ModLoader.ModNPC"));
		RefactorInstanceMethodCall("Terraria.ModLoader.Mod", "PrefixType",		ToFindTypeCall("Terraria.ModLoader.ModPrefix"));
		RefactorInstanceMethodCall("Terraria.ModLoader.Mod", "ProjectileType",	ToFindTypeCall("Terraria.ModLoader.ModProjectile"));
		RefactorInstanceMethodCall("Terraria.ModLoader.Mod", "TileEntityType",	ToFindTypeCall("Terraria.ModLoader.ModTileEntity"));
		RefactorInstanceMethodCall("Terraria.ModLoader.Mod", "TileType",		ToFindTypeCall("Terraria.ModLoader.ModTile"));
		RefactorInstanceMethodCall("Terraria.ModLoader.Mod", "WallType",		ToFindTypeCall("Terraria.ModLoader.ModWall"));

		//RefactorInstanceMethodCall("Terraria.ModLoader.Mod", "GetGoreSlot",		ToFindTypeCall("Terraria.ModLoader.ModGore")); // TODO needs more sophisicated rewriter that detects parameter content

		//RefactorStaticMethodCall("Terraria.ModLoader.ModGore", "GetGoreSlot",	ToFindTypeCall("Terraria.ModLoader.ModGore")); //todo, OnType ModContent, and same as Mod.GetGoreSlot

		RefactorInstanceMethodCall("Terraria.ModLoader.Mod", "RegisterHotKey",		ToStaticMethodCall("Terraria.ModLoader.KeybindLoader",				"RegisterKeybind",		targetBecomesFirstArg: true));
		RefactorInstanceMethodCall("Terraria.ModLoader.Mod", "AddTranslation",		ToStaticMethodCall("Terraria.ModLoader.LocalizationLoader",			"AddTranslation",		targetBecomesFirstArg: false));
		RefactorInstanceMethodCall("Terraria.ModLoader.Mod", "CreateTranslation",	ToStaticMethodCall("Terraria.ModLoader.LocalizationLoader",			"CreateTranslation",	targetBecomesFirstArg: true));
		RefactorInstanceMethodCall("Terraria.ModLoader.Mod", "AddBackgroundTexture",ToStaticMethodCall("Terraria.ModLoader.BackgroundTextureLoader",	"AddBackgroundTexture",	targetBecomesFirstArg: true));
		RefactorInstanceMethodCall("Terraria.ModLoader.Mod", "GetBackgroundSlot",	ToStaticMethodCall("Terraria.ModLoader.BackgroundTextureLoader",	"GetBackgroundSlot",	targetBecomesFirstArg: true));
		RefactorInstanceMethodCall("Terraria.ModLoader.Mod", "GetEquipTexture",		ToStaticMethodCall("Terraria.ModLoader.EquipLoader",				"GetEquipTexture",		targetBecomesFirstArg: true));
		RefactorInstanceMethodCall("Terraria.ModLoader.Mod", "GetEquipSlot",		ToStaticMethodCall("Terraria.ModLoader.EquipLoader",				"GetEquipSlot",			targetBecomesFirstArg: true));
		RefactorInstanceMethodCall("Terraria.ModLoader.Mod", "GetAccessorySlot",	ToStaticMethodCall("Terraria.ModLoader.EquipLoader",				"GetEquipSlot",			targetBecomesFirstArg: true));
		RefactorInstanceMethodCall("Terraria.ModLoader.Mod", "AddEquipTexture",		ConvertAddEquipTexture);
		RefactorInstanceMethodCall("Terraria.ModLoader.Mod", "CreateRecipe",		ToStaticMethodCall("Terraria.Recipe",								"Create",				targetBecomesFirstArg: false));

		RenameMethod("System.IO.BinaryReader",			from: "ReadVarInt",		to: "Read7BitEncodedInt");
		RenameMethod("System.IO.BinaryWriter",			from: "WriteVarInt",	to: "Write7BitEncodedInt");
		RenameMethod("Terraria.ModLoader.Mod",			from: "TextureExists",	to: "HasAsset");
		RenameMethod("Terraria.ModLoader.ModContent",	from: "TextureExists",	to: "HasAsset");

		RenameMethod("Terraria.ModLoader.ModPrefix", from: "GetPrefix", to: "GetPrefix", newType: "Terraria.ModLoader.PrefixLoader");

		RenameType(from: "Terraria.ModLoader.PlayerHooks",			to: "Terraria.ModLoader.PlayerLoader");
		RenameType(from: "Terraria.ModLoader.MusicPriority",		to: "Terraria.ModLoader.SceneEffectPriority");
		RenameType(from: "Terraria.ModLoader.SpawnCondition",		to: "Terraria.ModLoader.Utilities.SpawnCondition");
		RenameType(from: "Terraria.ModLoader.ModSurfaceBgStyle",	to: "Terraria.ModLoader.ModSurfaceBackgroundStyle");
		RenameType(from: "Terraria.ModLoader.ModUgBgStyle",			to: "Terraria.ModLoader.ModUndergroundBackgroundStyle");
		RenameType(from: "Terraria.ModLoader.PlayerDrawInfo",		to: "Terraria.DataStructures.PlayerDrawSet");

		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "position", to: "Position");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "itemLocation", to: "ItemLocation");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "drawHeldProjInFrontOfHeldItemAndBody", to: "heldProjOverHand");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "drawHair", to: "fullHair");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "drawAltHair", to: "hatHair");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "headArmorShader", to: "cHead");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "bodyArmorShader", to: "cBody");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "legArmorShader", to: "cLegs");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "handOnShader", to: "cHandOn");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "handOffShader", to: "cHandOff");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "backShader", to: "cBack");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "frontShader", to: "cFront");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "shoeShader", to: "cShoe");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "waistShader", to: "cWaist");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "shieldShader", to: "cShield");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "neckShader", to: "cNeck");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "faceShader", to: "cFace");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "balloonShader", to: "cBalloon");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "wingShader", to: "cWings");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "carpetShader", to: "cCarpet");

		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "hairColor", to: "colorHair");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "eyeWhiteColor", to: "colorEyeWhites");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "eyeColor", to: "colorEyes");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "faceColor", to: "colorHead");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "bodyColor", to: "colorBodySkin");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "legColor", to: "colorLegs");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "shirtColor", to: "colorShirt");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "underShirtColor", to: "colorUnderShirt");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "pantsColor", to: "colorPants");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "shoeColor", to: "colorShoes");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "upperArmorColor", to: "colorArmorHead");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "middleArmorColor", to: "colorArmorBody");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "mountColor", to: "colorMount");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "lowerArmorColor", to: "colorArmorLegs");

		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "legGlowMask", to: "legsGlowMask");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "legGlowMaskColor", to: "legsGlowColor");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "spriteEffects", to: "playerEffect");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "headOrigin", to: "headVect");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "bodyOrigin", to: "bodyVect");
		RenameInstanceField("Terraria.DataStructures.PlayerDrawSet", from: "legOrigin", to: "legVect");

		RenameType(from: "Terraria.ModLoader.ModRecipe", to: "Terraria.Recipe");
		RenameMethod("Terraria.Recipe", from: "AddRecipe", "Register");
	}
}