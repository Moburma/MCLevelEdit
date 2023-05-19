#Magic Carpet Level Editor by Moburma

#VERSION 0.2
#LAST MODIFIED: 19/05/2023

<#
.SYNOPSIS
   This script can read uncompressed Magic Carpet level files (LEV00xxx.DAT) and let users edit and save them back.

.DESCRIPTION    
    An editor for Magic Carpet 1 level files
    
.RELATED LINKS
    
    https://github.com/michaelhoward/MagicCarpetFileFormat
    
#>


#set temporary variables until a level is loaded
$filename = "No File Loaded"
$manatotal =  0
$manatarget = 0


function convert16bitint($Byteone, $Bytetwo) {
   

$converbytes16 = [byte[]]($Byteone,$Bytetwo)
$converted16 =[bitconverter]::ToInt16($converbytes16,0)

return $converted16

}

function convert32bitint($Byteone, $Bytetwo, $Bytethree, $ByteFour) {
   
$converbytes32 = [byte[]]($Byteone,$Bytetwo,$Bytethree,$ByteFour)
$converted32 =[bitconverter]::ToUInt32($converbytes32,0)

return $converted32

}

$zerobyte = [System.Convert]::ToString(0,16) -as [Byte] # Use this for zero byte entries going forward

$UnknownCombo = @("Blank")

function identifyThing ($class){ 

Switch ($class) {

    0{ return 'Unknown'}
    1{ return 'N/A'}
    2{ return 'Scenery'}
    3{ return 'Player Spawn'}
    4{ return 'N/A'}
    5{ return 'Creature'}
    6{ return 'N/A'}
    7{ return 'Weather'}
    8{ return 'N/A'}
    9{ return 'N/A'}
    10{ return 'Effect'}
    11{ return 'Switch'}
    12{ return 'Spell'}


}
}
$classcombo = @("Blank", "Unknown","N/A","Scenery","Player Spawn","Creature","Weather","Effect","Switch","Spell")

function reverseIdentifyThing ($class){ 

    Switch ($class) {
    
        'Unknown'{ return 0 }
        'N/A'{ return 1 }
        'Scenery'{ return 2 }
        'Player Spawn'{ return 3 }
 
        'Creature'{ return 5 }
  
        'Weather'{ return 7 }
        'Effect'{ return 10 }
        'Switch'{ return 11 }
        'Spell' { return 12 }
    
    
    }
    }


function identifyCreature ($model){ #Returns what the creature type name is

switch ($model)
{

    0{ return 'Dragon'}
    1{ return 'Vulture'}
    2{ return 'Bee'}
    3{ return 'Worm'}
    4{ return 'Archer'}
    5{ return 'Crab'}
    6{ return 'Kraken'}
    7{ return 'Troll'}
    8{ return 'Griffin'}
    9{ return 'Skeleton'}
    10{ return 'Emu'}
    11{ return 'Genie'}
    12{ return 'Builder'}
    13{ return 'Townie'}
    14{ return 'Trader'}
    15{ return 'Unknown'}
    16{ return 'Wyvern'}

    default { return "Unknown" }
  

}
}
function reverseIdentifycreature ($model){ #Returns what the creature type name is

    switch ($model)
    {
'Dragon'{ return 0 }
'Vulture'{ return 1 }
'Bee'{ return 2 }
'Worm'{ return 3 }
'Archer'{ return 4 }
'Crab'{ return 5 }
'Kraken'{ return 6 }
'Troll'{ return 7 }
'Griffin'{ return 8 }
'Skeleton'{ return 9 }
'Emu'{ return 10 }
'Genie'{ return 11 }
'Builder'{ return 12 }
'Townie'{ return 13 }
'Trader'{ return 14 }
'Unknown'{ return 15 }
'Wyvern'{ return 16 }
    }
}

$creatureCombo = @("Blank", "Dragon", "Vulture", "Bee", "Worm", "Archer", "Crab", "Kraken", "Troll", "Griffin", "Skeleton", "Emu", "Genie", "Builder", "Townie", "Trader", "Unknown", "Wyvern")

function identifyscenery($model){ #Returns what the scenery type name is

Switch ($model){  

    0{ return 'Tree'}
    1{ return 'Standing Stone'}
    2{ return 'Dolmen'}
    3{ return 'Bad Stone'}
    4{ return '2D Dome'}
    5{ return '2D Dome'}
    default { return "Unknown" }

}


}

function reverseIdentifyscenery($model){ #Returns what the scenery type number is

    Switch ($model){  
    
        'Tree'{ return 0 }
        'Standing Stone'{ return 1 }
        'Dolmen'{ return 2 }
        'Bad Stone'{ return 3 }
        '2D Dome'{ return 4 }
        
    
    }
    
    
    }
$sceneryCombo = @("Blank", "Tree", "Standing Stone", "Dolmen", "Bad Stone", "2D Dome")

function identifyEffect($model){ #Returns what the effect name is

Switch ($model){  

    0{ return 'Unknown'}
    1{ return 'Big explosion'}
    2{ return 'Unknown'}
    3{ return 'Unknown'}
    4{ return 'Unknown'}
    5{ return 'Splash'}
    6{ return 'Fire'}
    7{ return 'Unknown'}
    8{ return 'Mini Volcano'}
    9{ return 'Volcano'}
    10{ return 'Mini crater'}
    11{ return 'Crater'}
    12{ return 'Unknown'}
    13{ return 'White smoke'}
    14{ return 'Black smoke'}
    15{ return 'Earthquake'}
    16{ return 'Unknown'}
    17{ return 'Meteor'}
    18{ return 'Unknown'}
    19{ return 'Unknown'}
    20{ return 'Unknown'}
    21{ return 'Steal Mana'}
    22{ return 'Unknown'}
    23{ return 'Lightning'}
    24{ return 'Rain of Fire'}
    25{ return 'Unknown'}
    26{ return 'Unknown'}
    27{ return 'Unknown'}
    28{ return 'Wall'}
    29{ return 'Path'}
    30{ return 'Unknown'}
    31{ return 'Canyon'}
    32{ return 'Unknown'}
    33{ return 'Unknown'}
    34{ return 'Teleport'}
    35{ return 'Unknown'}
    36{ return 'Unknown'}
    37{ return 'Unknown'}
    38{ return 'Unknown'}
    39{ return 'Mana Ball'}
    40{ return 'Unknown'}
    41{ return 'Unknown'}
    42{ return 'Unknown'}
    43{ return 'Unknown'}
    44{ return 'Unknown'}
    45{ return 'Villager Building'}
    46{ return 'Unknown'}
    47{ return 'Unknown'}
    48{ return 'Unknown'}
    49{ return 'Unknown'}
    50{ return 'Ridge Node'}
    51{ return 'Unknown'}
    52{ return 'Crab Egg'}
    default { return "Unknown" }

}

}

function reverseIdentifyEffect($model){ #Returns what the effect name is

    Switch ($model){  
    
        'Unknown'{ return 0 }
        'Big explosion'{ return 1 }
        'Splash'{ return 5 }
        'Fire'{ return 6 }
        'Mini Volcano'{ return 8 }
        'Volcano'{ return 9 }
        'Mini crater'{ return 10 }
        'Crater'{ return 11 }
        'White smoke'{ return 13 }
        'Black smoke'{ return 14 }
        'Earthquake'{ return 15 }
        'Meteor'{ return 17 }
        'Steal Mana'{ return 21 }
        'Lightning'{ return 23 }
        'Rain of Fire'{ return 24 }
        'Wall'{ return 28 }
        'Path'{ return 29 }
        'Canyon'{ return 31 }
        'Teleport'{ return 34 }
        'Mana Ball'{ return 39 }
        'Villager Building'{ return 45 }
        'Ridge Node'{ return 50 }
        'Crab Egg'{ return 52 }

    }
    
    }
$effectCombo = @("Blank", "Big explosion", "Unknown", "Splash", "Fire", "Mini Volcano", "Volcano", "Mini crater", "Crater", "White smoke", "Black smoke", "Earthquake", "Meteor", "Steal Mana", "Lightning", "Rain of Fire", "Wall", "Path", "Canyon", "Teleport", "Mana Ball", "Villager Building", "Ridge Node", "Crab Egg")

function identifySwitch($model){ #Returns what kind of switch this is

Switch ($model){  
 
    0{ return 'Hidden Inside'}
    1{ return 'Hidden outside'}
    2{ return 'Hidden Inside re'}
    3{ return 'Unknown'}
    4{ return 'On victory'}
    5{ return 'Death Inside'}
    6{ return 'Death Outside'}
    7{ return 'Death inside re'}
    8{ return 'Unknown'}
    9{ return 'Obvious Inside'}
    10{ return 'Obvious outside'}
    11{ return 'Unknown'}
    12{ return 'Unknown'}
    13{ return 'Dragon'}
    14{ return 'Vulture'}
    15{ return 'Bee'}
    16{ return 'None'}
    17{ return 'Archer'}
    18{ return 'Crab'}
    19{ return 'Kraken'}
    20{ return 'Troll'}
    21{ return 'Griffon'}
    22{ return 'Unknown'}
    23{ return 'Unknown'}
    24{ return 'Genie'}
    25{ return 'Unknown'}
    26{ return 'Unknown'}
    27{ return 'Unknown'}
    28{ return 'Unknown'}
    29{ return 'Wyvern'}
    30{ return 'Creature all'}
    default { return "Unknown" }
}

}

function reverseIdentifySwitch($model){ #Returns what kind of switch this is

    Switch ($model){  
     
        'Hidden Inside'{ return 0 }
        'Hidden outside'{ return 1 }
        'Hidden Inside re'{ return 2 }
        'Unknown'{ return 3 }
        'On victory'{ return 4 }
        'Death Inside'{ return 5 }
        'Death Outside'{ return 6 }
        'Death inside re'{ return 7 }
        'Unknown'{ return 8 }
        'Obvious Inside'{ return 9 }
        'Obvious outside'{ return 10 }
        'Unknown'{ return 11 }
        'Unknown'{ return 12 }
        'Dragon'{ return 13 }
        'Vulture'{ return 14 }
        'Bee'{ return 15 }
        'None'{ return 16 }
        'Archer'{ return 17 }
        'Crab'{ return 18 }
        'Kraken'{ return 19 }
        'Troll'{ return 20 }
        'Griffon'{ return 21 }
        'Unknown'{ return 22 }
        'Unknown'{ return 23 }
        'Genie'{ return 24 }
        'Unknown'{ return 25 }
        'Unknown'{ return 26 }
        'Unknown'{ return 27 }
        'Unknown'{ return 28 }
        'Wyvern'{ return 29 }
        'Creature all'{ return 30 }

    }
    
    }

$switchCombo = @("Blank", "Hidden Inside", "Hidden outside", "Hidden Inside re", "Unknown", "On victory", "Death Inside", "Death Outside", "Death inside re", "Obvious Inside", "Obvious outside", "Dragon", "Vulture", "Bee", "None", "Archer", "Crab", "Kraken", "Troll", "Griffon", "Genie", "Wyvern", "Creature all")

function identifyspawn ($model){ 

Switch ($model) {

    0{ return 'Unknown'}
    1{ return 'Unknown'}
    2{ return 'Unknown'}
    3{ return 'Unknown'}
    4{ return 'Flyer1'}
    5{ return 'Flyer2'}
    6{ return 'Flyer3'}
    7{ return 'Flyer4'}
    8{ return 'Flyer5'}
    9{ return 'Flyer6'}
    10{ return 'Flyer7'}
    11{ return 'Flyer8'}


}
}

function reverseIdentifyspawn ($model){ 

    Switch ($model) {
    
        'Unknown'{ return 0 }
        'Flyer1'{ return 4 }
        'Flyer2'{ return 5 }
        'Flyer3'{ return 6 }
        'Flyer4'{ return 7 }
        'Flyer5'{ return 8 }
        'Flyer6'{ return 9 }
        'Flyer7'{ return 10 }
        'Flyer8'{ return 11 }
    
    }
    }

$spawnCombo = @("Blank", "Unknown", "Flyer1", "Flyer2", "Flyer3", "Flyer4", "Flyer5", "Flyer6", "Flyer7", "Flyer8")
function identifyspell ($model){ 

Switch ($model) {

    0{ return 'Fireball'}
    1{ return 'Heal'}
    2{ return 'Speed Up'}
    3{ return 'Possession'}
    4{ return 'Shield'}
    5{ return 'Beyond Sight'}
    6{ return 'Earthquake'}
    7{ return 'Meteor'}
    8{ return 'Volcano'}
    9{ return 'Crater'}
    10{ return 'Teleport'}
    11{ return 'Duel'}
    12{ return 'Invisible'}
    13{ return 'Steal Mana'}
    14{ return 'Rebound'}
    15{ return 'Lightning'}
    16{ return 'Castle'}
    17{ return 'Skeleton'}
    18{ return 'Thunderbolt'}
    19{ return 'Mana Magnet'}
    20{ return 'Fire Wall'}
    21{ return 'Reverse Speed'}
    22{ return 'Global Death'}
    23{ return 'Rapid Fireball'}
    
}
}

function reverseIdentifyspell ($model){ 

    Switch ($model) {
    
        'Fireball'{ return 0 }
        'Heal'{ return 1 }
        'Speed Up'{ return 2 }
        'Possession'{ return 3 }
        'Shield'{ return 4 }
        'Beyond Sight'{ return 5 }
        'Earthquake'{ return 6 }
        'Meteor'{ return 7 }
        'Volcano'{ return 8 }
        'Crater'{ return 9 }
        'Teleport'{ return 10 }
        'Duel'{ return 11 }
        'Invisible'{ return 12 }
        'Steal Mana'{ return 13 }
        'Rebound'{ return 14 }
        'Lightning'{ return 15 }
        'Castle'{ return 16 }
        'Skeleton'{ return 17 }
        'Thunderbolt'{ return 18 }
        'Mana Magnet'{ return 19 }
        'Fire Wall'{ return 20 }
        'Reverse Speed'{ return 21 }
        'Global Death'{ return 22 }
        'Rapid Fireball'{ return 23 }
        
        
    }
    }

$spellCombo = @("Blank","Fireball", "Heal", "Speed Up", "Possession", "Shield", "Beyond Sight", "Earthquake", "Meteor", "Volcano", "Crater", "Teleport", "Duel", "Invisible", "Steal Mana", "Rebound", "Lightning", "Castle", "Skeleton", "Thunderbolt", "Mana Magnet", "Fire Wall", "Reverse Speed", "Global Death", "Rapid Fireball")

function identifyweather($model){ #Returns what the weather type is

    Switch ($model){  
    
        0{ return 'Unknown'}
        1{ return 'Unknown'}
        2{ return 'Unknown'}
        3{ return 'Unknown'}
        4{ return 'Wind'}
        default { return "Unknown" }
    }
    }

 function reverseIdentifyweather($model){ #Returns what the weather type is

    Switch ($model){  
        
         'Unknown'{ return 0 }
          'Wind'{ return 4 }
        
    }
    }

$weatherCombo = @("Blank", "Unknown", "Wind")

function Wizardname($wizardname){ #Returns what the Wizard's name is

Switch ($wizardname){  

    1{ return 'Player'}
    2{ return 'Vodor'}
    3{ return 'Gryshnak'}
    4{ return 'Mahmoud'}
    5{ return 'Syed'}
    6{ return 'Raschid'}
    7{ return 'Alhabbal'}
    8{ return 'Scheherazade'}
    

}

}

$presentCombo = @("Yes","No")

function thingColour($thingColour){
    Switch ($thingColour){  

     2{ return 'Green'}
     3{ return 'Yellow'}
     5{ return 'Red'}
     7{ return 'Blue'}
     10{ return 'Cyan'}
     11{ return 'White'}
     12{ return 'Purple'}

    }


}

function zeroPad ($repetitions) {
    $zerobytespadding = ""
    [byte[]]$zerobytespadding
    $zerobyte = [System.Convert]::ToString(0,16) -as [Byte]

    DO{
        $zerobytespadding = @($zerobytespadding + $zerobyte) -as [byte[]]
        
        $repetitions = $repetitions -1
       
    } UNTIL ($repetitions -eq 0)

    Return $zerobytespadding
}

function onePad ($repetitions) {
    $onebytespadding = ""
    [byte[]]$onebytespadding
    $onebyte = [System.Convert]::ToString(1,16) -as [Byte]

    DO{
        $onebytespadding = @($onebytespadding + $onebyte) -as [byte[]]
        
        $repetitions = $repetitions -1
       
    } UNTIL ($repetitions -eq 0)

    Return $onebytespadding
}


function SaveFile(){
   
    [void][System.Reflection.Assembly]::LoadWithPartialName("System.windows.forms") 

    $OpenFileDialog = New-Object System.Windows.Forms.SaveFileDialog
    $OpenFileDialog.initialDirectory = $scriptdir 
    $OpenFileDialog.filter = "DAT files (*.DAT)| *.DAT"
    $OpenFileDialog.ShowDialog() |  Out-Null

    $outputfile = $OpenFileDialog.filename
    write-host $outputfile
    
    if ($outputfile -eq ""){ # User cancelled save file requester
        return 
    }
    $zerobyte = [System.BitConverter]::GetBytes(0)

    [byte[]]$MapGenOutput 
     $PaddedBytes = [System.BitConverter]::GetBytes($mapGenDataTable.Rows[0].ManaTotal)
    $MapGenOutput  += $paddedBytes[0..3]        
    $PaddedBytes = [System.BitConverter]::GetBytes($mapGenDataTable.Rows[0].Seed)
    $MapGenOutput  += $paddedBytes[0..1]
    $MapGenOutput  += $zerobyte[0..1]
    $PaddedBytes = [System.BitConverter]::GetBytes($mapGenDataTable.Rows[0].Offset)
    $MapGenOutput  += $paddedBytes[0..1]
    $MapGenOutput  += $zerobyte[0..1]
    $PaddedBytes = [System.BitConverter]::GetBytes($mapGenDataTable.Rows[0].Raise)
    $MapGenOutput  += $paddedBytes[0..1]
    $MapGenOutput  += $zerobyte[0..1]
    $PaddedBytes = [System.BitConverter]::GetBytes($mapGenDataTable.Rows[0].Gnarl)
    $MapGenOutput  += $paddedBytes[0..1]
    $MapGenOutput  += $zerobyte[0..1]
    $PaddedBytes = [System.BitConverter]::GetBytes($mapGenDataTable.Rows[0].River)
    $MapGenOutput  += $paddedBytes[0..1]
    $MapGenOutput  += $zerobyte[0..1]
    $PaddedBytes = [System.BitConverter]::GetBytes($mapGenDataTable.Rows[0].Sourc)
    $MapGenOutput  += $paddedBytes[0..1]
    $MapGenOutput  += $zerobyte[0..1]
    $PaddedBytes = [System.BitConverter]::GetBytes($mapGenDataTable.Rows[0].SnLin)
    $MapGenOutput  += $paddedBytes[0..1]
    $MapGenOutput  += $zerobyte[0..1]
    $PaddedBytes = [System.BitConverter]::GetBytes($mapGenDataTable.Rows[0].SnFlt)
    $MapGenOutput  += $paddedBytes[0..1]
    $MapGenOutput  += $zerobyte[0..1]
    $PaddedBytes = [System.BitConverter]::GetBytes($mapGenDataTable.Rows[0].BhLin)
    $MapGenOutput  += $paddedBytes[0..1]
    $MapGenOutput  += $zerobyte[0..1]
    $PaddedBytes = [System.BitConverter]::GetBytes($mapGenDataTable.Rows[0].BhFlt)
    $MapGenOutput  += $paddedBytes[0..1]
    $MapGenOutput  += $zerobyte[0..1]
    $PaddedBytes = [System.BitConverter]::GetBytes($mapGenDataTable.Rows[0].RkSte)
    $MapGenOutput  += $paddedBytes[0..1]
    $MapGenOutput  += $zerobyte[0..1]

    set-Content $outputfile -Value $MapGenOutput -Encoding Byte


    $zerobytespadding = zeroPad(1041)   # This is probably really stupid, review when porting to C#
    add-Content $outputfile -Value $zerobytespadding -Encoding Byte

    [byte[]] $Fileoutput

  
    foreach ($drow in $Datatable)
    {   
    
        $PaddedBytes = [System.BitConverter]::GetBytes($drow.class)
        $Fileoutput += $paddedBytes[0..1]
        
        $PaddedBytes = [System.BitConverter]::GetBytes($drow.Model)
        $Fileoutput += $paddedBytes[0..1]

        $PaddedBytes = [System.BitConverter]::GetBytes($drow.XPos)
        $Fileoutput += $paddedBytes[0..1]

       $PaddedBytes = [System.BitConverter]::GetBytes($drow.YPos)
        $Fileoutput += $paddedBytes[0..1]

       $PaddedBytes = [System.BitConverter]::GetBytes($drow.DisId)
       $Fileoutput += $paddedBytes[0..1]

       $PaddedBytes = [System.BitConverter]::GetBytes($drow.SwiSz)
       $Fileoutput += $paddedBytes[0..1]

       $PaddedBytes = [System.BitConverter]::GetBytes($drow.SwiId)
       $Fileoutput += $paddedBytes[0..1]

       $PaddedBytes = [System.BitConverter]::GetBytes($drow.Parent)
       $Fileoutput += $paddedBytes[0..1]

       $PaddedBytes = [System.BitConverter]::GetBytes($drow.Child)
       $Fileoutput += $paddedBytes[0..1]

            
    }
    add-Content $outputfile -Value $Fileoutput  -Encoding Byte
    
    #Save Wizard data

    [byte[]] $wizOutput

    $wPadding = zeropad(72)
    $oPadding = onepad(100)

    #$wizOutput  += $zerobyte[0..1] 

    foreach ($wRow in $WizDatatable)
    {   
        $wizOutput  += $zerobyte[0..3] # 4 bytes buffer
        $cBytes = [System.BitConverter]::GetBytes($wrow.Aggression)
        $wizOutput += $cBytes[0..1]
        $wizOutput  += $zerobyte[0..1]
        $cBytes = [System.BitConverter]::GetBytes($wrow.Perception)
        $wizOutput += $cBytes[0..1]
        $wizOutput  += $zerobyte[0..1]
        $cBytes = [System.BitConverter]::GetBytes($wrow.Reflexes)
        $wizOutput += $cBytes[0..1]
        $wizOutput  += $zerobyte[0..1] #end of APR 

        $cBytes = [System.BitConverter]::GetBytes($wrow.Fireball)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.Shield)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.Accelerate)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.Possession)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.Health)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.BeyondSight)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.Earthquake)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.Meteor)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.Volcano)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.Crater)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.Teleport)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.Duel)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.Invisible)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.StealMana)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.Rebound)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.Lightning)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.Castle)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.UndeadArmy)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.LightningStorm)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.ManaMagnet)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.WallofFire)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.ReverseAcceleration)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.GlobalDeath)
        $wizOutput += $cBytes[0]
        $cBytes = [System.BitConverter]::GetBytes($wrow.RapidFireball)
        $wizOutput += $cBytes[0]

        
         #arbitrary padding values at end of each Wizard
        $wizOutput += $wPadding
        $wizOutput  += $zerobyte[0..2]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat2)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat3)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat4)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat5)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat6)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat7)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat8)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat9)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat10)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat11)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat12)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat13)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat14)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat15)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat16)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat17)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat18)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat19)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat20)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat21)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat22)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat23)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat24)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat25)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat26)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat27)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat28)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat29)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat30)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat31)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat32)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat33)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat34)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat35)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat36)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat37)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat38)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat39)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat40)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat41)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat42)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat43)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat44)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat45)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat46)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat47)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat48)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat49)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat50)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat51)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat52)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat53)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat54)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat55)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat56)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat57)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat58)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat59)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat60)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat61)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat62)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat63)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat64)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat65)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat66)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat67)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat68)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat69)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat70)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat71)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat72)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat73)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat74)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat75)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat76)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat77)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat78)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat79)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat80)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat81)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat82)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat83)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat84)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat85)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat86)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat87)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat88)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat89)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat90)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat91)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat92)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat93)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat94)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat95)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat96)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat97)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat98)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat99)
        $wizOutput += $aiBytes[0]
        $aiBytes = [System.BitConverter]::GetBytes($wrow.aistat100)
        $wizOutput += $aiBytes[0]
        
       

    }


    add-Content $outputfile -Value $WizOutput -Encoding Byte

    # Level Data 
    [byte[]] $levelDataOutput
    
    $ldBytes = [System.BitConverter]::GetBytes($levelDataTable.rows[0][0])
    $levelDataOutput  += $ldBytes[0..1]
    $ldBytes = [System.BitConverter]::GetBytes($levelDataTable.rows[0][1])
    $levelDataOutput  += $ldBytes[0..1]
    $ldBytes = [System.BitConverter]::GetBytes($wizDetails.Rows[0].Cells[4].Value)
    $levelDataOutput  += $ldBytes[0]
    $ldBytes = [System.BitConverter]::GetBytes($wizDetails.Rows[1].Cells[4].Value)
    $levelDataOutput  += $ldBytes[0]
    $ldBytes = [System.BitConverter]::GetBytes($wizDetails.Rows[2].Cells[4].Value)
    $levelDataOutput  += $ldBytes[0]
    $ldBytes = [System.BitConverter]::GetBytes($wizDetails.Rows[3].Cells[4].Value)
    $levelDataOutput  += $ldBytes[0]
    $ldBytes = [System.BitConverter]::GetBytes($wizDetails.Rows[4].Cells[4].Value)
    $levelDataOutput  += $ldBytes[0]
    $ldBytes = [System.BitConverter]::GetBytes($wizDetails.Rows[5].Cells[4].Value)
    $levelDataOutput  += $ldBytes[0]
    $ldBytes = [System.BitConverter]::GetBytes($wizDetails.Rows[6].Cells[4].Value)
    $levelDataOutput  += $ldBytes[0]
    $ldBytes = [System.BitConverter]::GetBytes($wizDetails.Rows[7].Cells[4].Value)
    $levelDataOutput  += $ldBytes[0]

    add-Content $outputfile -Value $levelDataOutput -Encoding Byte

    write-host "Done"
    Add-Type -AssemblyName PresentationCore,PresentationFramework
    $SaveButtonType = [System.Windows.MessageBoxButton]::Ok
    $SaveMessageboxTitle = "Save Level File"
    $SaveMessageboxbody = "Level Saved Successfully"
    $SaveMessageIcon = [System.Windows.MessageBoxImage]::Information 
    [void][System.Windows.MessageBox]::Show($SaveMessageboxbody,$SaveMessageboxTitle,$SaveButtonType,$Savemessageicon)  

}


function SpellLoadout($rownum){
$spellList = ""

if ($WizDetails.Rows[$rownum].Cells[7].Value -eq 1){
$spellList = $spellList + "Fireball, "
} 

if ($WizDetails.Rows[$rownum].Cells[8].Value -eq 1){
$spellList = $spellList +"Shield, "
} 

if ($WizDetails.Rows[$rownum].Cells[9].Value -eq 1){
$spellList = $spellList + "Accelerate, "
} 

if ($WizDetails.Rows[$rownum].Cells[10].Value -eq 1){
$spellList = $spellList + "Possession, "
} 

if ($WizDetails.Rows[$rownum].Cells[11].Value -eq 1){
$spellList = $spellList + "Health, "
} 

if ($WizDetails.Rows[$rownum].Cells[12].Value -eq 1){
$spellList = $spellList + "Beyond Sight, "
} 

if ($WizDetails.Rows[$rownum].Cells[13].Value -eq 1){
$spellList = $spellList + "Earthquake, "
}

if ($WizDetails.Rows[$rownum].Cells[14].Value -eq 1){
$spellList = $spellList + "Meteor, "
}

if ($WizDetails.Rows[$rownum].Cells[15].Value -eq 1){
$spellList = $spellList + "Volcano, "
}

if ($WizDetails.Rows[$rownum].Cells[16].Value -eq 1){
$spellList = $spellList + "Crater, "
}

if ($WizDetails.Rows[$rownum].Cells[17].Value -eq 1){
$spellList = $spellList + "Teleport, "
}

if ($WizDetails.Rows[$rownum].Cells[18].Value -eq 1){
$spellList = $spellList + "Duel, "
}

if ($WizDetails.Rows[$rownum].Cells[19].Value -eq 1){
$spellList = $spellList + "Invisible, "
}

if ($WizDetails.Rows[$rownum].Cells[20].Value -eq 1){
$spellList = $spellList + "Steal Mana, "
}

if ($WizDetails.Rows[$rownum].Cells[21].Value -eq 1){
$spellList = $spellList + "Rebound, "
}

if ($WizDetails.Rows[$rownum].Cells[22].Value -eq 1){
$spellList = $spellList + "Lightning, "
}

if ($WizDetails.Rows[$rownum].Cells[23].Value -eq 1){
$spellList = $spellList + "Castle, "
}

if ($WizDetails.Rows[$rownum].Cells[24].Value -eq 1){
$spellList = $spellList + "Undead Army, "
}

if ($WizDetails.Rows[$rownum].Cells[25].Value -eq 1){
$spellList = $spellList + "Lightning Storm, "
}

if ($WizDetails.Rows[$rownum].Cells[26].Value -eq 1){
$spellList = $spellList + "Mana Magnet, "
}

if ($WizDetails.Rows[$rownum].Cells[27].Value -eq 1){
$spellList = $spellList + "Wall of Fire, "
}

if ($WizDetails.Rows[$rownum].Cells[28].Value -eq 1){
$spellList = $spellList + "Reverse Acceleration, "
}

if ($WizDetails.Rows[$rownum].Cells[29].Value -eq 1){
$spellList = $spellList + "Global Death, "
}

if ($WizDetails.Rows[$rownum].Cells[30].Value -eq 1){
$spellList = $spellList + "Rapid Fireball"
}

Return $spellList

}

function LoadLevel(){


    
    $drawmap = 1
    [void][System.Reflection.Assembly]::LoadWithPartialName("System.Drawing")
    $global:bmp = New-Object System.Drawing.Bitmap(256, 256)
    
    [void][System.Reflection.Assembly]::LoadWithPartialName("System.windows.forms") 

    $OpenFileDialog = New-Object System.Windows.Forms.OpenFileDialog
    $OpenFileDialog.initialDirectory = $scriptdir
    $OpenFileDialog.filter = "DAT files (*.DAT)| *.DAT"
    $OpenFileDialog.ShowDialog() |  Out-Null

    $filename = $OpenFileDialog.filename

    if ($filename -eq ""){ # User cancelled load file requester
        return 
    }

    $FileOnly = [io.path]::GetFileName("$filename")
    $FileOnly = $FileOnly.Substring(0,$FileOnly.Length-4)
    

    $levfile = Get-Content $filename -Encoding Byte -ReadCount 0 #Load the actual file

    if ($levfile[0] -eq 82 -and $levfile[1] -eq 78 -and $levfile[2] -eq 67 ){

        Add-Type -AssemblyName PresentationCore,PresentationFramework
        $RNCButtonType = [System.Windows.MessageBoxButton]::Ok
        $RNCMessageboxTitle = "RNC File Detected"
        $RNCMessageboxbody = "Compressed level detected! You must uncompress this file first"
        $RNCMessageIcon = [System.Windows.MessageBoxImage]::Error
        [void][System.Windows.MessageBox]::Show($RNCMessageboxbody,$RNCMessageboxTitle,$RNCButtonType,$RNCmessageicon)  
        return
    }

    $thingcount = 1999
    $counter = 0
    $Wnumber = 1
    #Check File type

    $manatarget = $levfile[38800]
    $numwizards = $levfile[38802] 

    $MapgenDatatable.Rows.Clear();  #Clear out all datatables in case we already had a level open
    $Datatable.Rows.Clear();
    $WizDatatable.Rows.Clear();
    $levelDataTable.Rows.Clear();

    #Get Mapgen variables

    $ManaTotal = convert32bitint $levfile[0] $levfile[1] $levfile[2] $levfile[3]
    $Seed = convert16bitint $levfile[4] $levfile[5] 
    $Offset = convert16bitint $levfile[8] $levfile[9] 
    $Raise = convert16bitint $levfile[12] $levfile[13] 
    $Gnarl = convert16bitint $levfile[16] $levfile[17] 
    $River = convert16bitint $levfile[20] $levfile[21] 
    $Sourc = convert16bitint $levfile[24] $levfile[25] 
    $SnLin = convert16bitint $levfile[28] $levfile[29] 
    $SnFlt = convert16bitint $levfile[32] $levfile[33] 
    $BhLin = convert16bitint $levfile[36] $levfile[37] 
    $BhFlt = convert16bitint $levfile[40] $levfile[41] 
    $RkSte = convert16bitint $levfile[44] $levfile[45] 

    $mGRow = $mapGenDataTable.NewRow()  

    $mGRow.ManaTotal =  ($ManaTotal)
    $mGRow.Seed =  ($Seed)
    $mGRow.Offset =  ($Offset)
    $mGRow.Raise =  ($Raise)
    $mGRow.Gnarl =  ($Gnarl)
    $mGRow.River =  ($River)
    $mGRow.Sourc =  ($Sourc)
    $mGRow.SnLin =  ($SnLin)
    $mGRow.SnFlt =  ($SnFlt)
    $mGRow.BhLin =  ($BhLin)
    $mGRow.BhFlt =  ($BhFlt)
    $mGRow.RkSte =  ($RkSte)

    $mapGenDataTable.Rows.Add($mGRow)

   # Level Datatable

   $row = $levelDatatable.NewRow()  

   $row.ManaTarget =  ($manatarget)
   $row.NumWizards =  ($numwizards)

   $levelDatatable.Rows.Add($row)



    $fpos = 1090 # Move on to Thing data

    DO
    {

    $counter = $counter +1

    #Get Thing entries

    $ThingNo = $counter
    $class =  convert16bitint $levfile[$fpos] $levfile[$fpos+1]
    $ThingType = identifything $class
    $Model =  convert16bitint $levfile[$fpos+2] $levfile[$fpos+3]
    $Xpos = convert16bitint $levfile[$fpos+4] $levfile[$fpos+5]
    $Ypos =  convert16bitint $levfile[$fpos+6] $levfile[$fpos+7]
    $DisId = convert16bitint $levfile[$fpos+8] $levfile[$fpos+9]
    $SwiSz = convert16bitint $levfile[$fpos+10] $levfile[$fpos+11]
    $SwiId = convert16bitint $levfile[$fpos+12] $levfile[$fpos+13] 
    $Parent = convert16bitint $levfile[$fpos+14] $levfile[$fpos+15]
    $Child =  convert16bitint $levfile[$fpos+16] $levfile[$fpos+17]


    if ($class -eq 0){
    $ThingName = "Blank"
    }

    if ($class -eq 2){ 
    $ThingName = identifyscenery $model
        if ($drawmap -eq 1){
        $bmp.SetPixel($xpos, $ypos, 'Green')
        }
    }
    if ($class -eq 3){ 
    $ThingName = identifyspawn $model
        if ($drawmap -eq 1){
        $bmp.SetPixel($xpos, $ypos, 'Yellow')
        }
    }
    if ($class -eq 5){ 
    $ThingName = identifycreature $model
        if ($drawmap -eq 1){
        $bmp.SetPixel($xpos, $ypos, 'Red')
        }
    }
    if ($class -eq 7){ 
    $ThingName = identifyweather $model
    }
    if ($class -eq 10){ 
    $ThingName = identifyeffect $model
    if ($drawmap -eq 1){
        $bmp.SetPixel($xpos, $ypos, 'Cyan')
        }

    }
    if ($class -eq 11){ 
    $ThingName = identifyswitch $model
    if ($drawmap -eq 1){
        $bmp.SetPixel($xpos, $ypos, 'white')
        }

    }
    if ($class -eq 12){ 
    $ThingName = identifyspell $model
        if ($drawmap -eq 1){
        $bmp.SetPixel($xpos, $ypos, 'Purple')
        }
    }


    #format Some Data 
    if($disid -eq -1){
        $disid = 65535
        }
        
    if($swiid -eq -1){
        $swiid = 65535
        }
        

 
    #Define Thing datatable rows

    $row = $datatable.NewRow()  

    $row.thingno =  ($thingno)
    $row.class =  ($class)
    $row.ThingTypeHidden =  ($ThingType)
    $row.Model =  ($Model)
    $row.ThingNameHidden =  ($Thingname)
    $row.XPos =  ($XPos)
    $row.YPos =  ($YPos)
    $row.DisId =  ($DisId)
    $row.swiSz =  ($swisz)
    $row.SwiId =  ($SwiId)
    $row.parent =  ($parent)
    $row.child =  ($child)
    $datatable.Rows.Add($row)

    $ThingCount = $ThingCount - 1


    $fpos = $fpos + 18
    }
    UNTIL ($ThingCount -eq 0)

    #Load Wizard Data
    $fpos = 37076
    
    $CLevel = 38804

    DO
    {

    $Wname =  WizardName $Wnumber
    $Aggression =  $levfile[$fpos]
    $Perception =  $levfile[$fpos+4] 
    $Reflexes =   $levfile[$fpos+8]
    $CastleLevel = $levfile[$CLevel]

    if ($wnumber -eq 1){  #Player
    $spellstart = 37088
    $aiStart = 37188
    }
    Elseif ($wnumber -eq 2){
    $spellstart =  37304
    $aiStart = 37404
    }
    Elseif ($wnumber -eq 3){
    $spellstart =  37520
    $aiStart = 37620
    }
    Elseif ($wnumber -eq 4){
    $spellstart =  37736
    $aiStart = 37836
    }
    Elseif ($wnumber -eq 5){
    $spellstart =  37952
    $aiStart = 38052
    }
    Elseif ($wnumber -eq 6){
    $spellstart =  38168
    $aiStart = 38268
    }
    Elseif ($wnumber -eq 7){
    $spellstart = 38384
    $aiStart = 38484
    }
    Elseif ($wnumber -eq 8){
    $spellstart = 38600
    $aiStart = 38700
    }

    $Fireball = $levfile[$spellstart]
    $Shield = $levfile[$spellstart+1]
    $Accelerate = $levfile[$spellstart+2]
    $Possession = $levfile[$spellstart+3]
    $Health = $levfile[$spellstart+4]
    $BeyondSight = $levfile[$spellstart+5]
    $Earthquake = $levfile[$spellstart+6]
    $Meteor = $levfile[$spellstart+7]
    $Volcano = $levfile[$spellstart+8]
    $Crater = $levfile[$spellstart+9]
    $Teleport = $levfile[$spellstart+10]
    $Duel = $levfile[$spellstart+11]
    $Invisible = $levfile[$spellstart+12]
    $StealMana = $levfile[$spellstart+13]
    $Rebound = $levfile[$spellstart+14]
    $Lightning = $levfile[$spellstart+15]
    $Castle = $levfile[$spellstart+16]
    $UndeadArmy = $levfile[$spellstart+17]
    $LightningStorm = $levfile[$spellstart+18]
    $ManaMagnet = $levfile[$spellstart+19]
    $WallofFire = $levfile[$spellstart+20]
    $ReverseAcceleration = $levfile[$spellstart+21]
    $GlobalDeath = $levfile[$spellstart+22]
    $RapidFireball = $levfile[$spellstart+23]

    $aistat = $levfile[$aiStart]
    $aistat2 =  $levfile[$aiStart+1]
    $aistat3 =  $levfile[$aiStart+2]
    $aistat4 =  $levfile[$aiStart+3]
    $aistat5 =  $levfile[$aiStart+4]
    $aistat6 =  $levfile[$aiStart+5]
    $aistat7 =  $levfile[$aiStart+6]
    $aistat8 =  $levfile[$aiStart+7]
    $aistat9 =  $levfile[$aiStart+8]
    $aistat10 =  $levfile[$aiStart+9]
    $aistat11 =  $levfile[$aiStart+10]
    $aistat12 =  $levfile[$aiStart+11]
    $aistat13 =  $levfile[$aiStart+12]
    $aistat14 =  $levfile[$aiStart+13]
    $aistat15 =  $levfile[$aiStart+14]
    $aistat16 =  $levfile[$aiStart+15]
    $aistat17 =  $levfile[$aiStart+16]
    $aistat18 =  $levfile[$aiStart+17]
    $aistat19 =  $levfile[$aiStart+18]
    $aistat20 =  $levfile[$aiStart+19]
    $aistat21 =  $levfile[$aiStart+20]
    $aistat22 =  $levfile[$aiStart+21]
    $aistat23 =  $levfile[$aiStart+22]
    $aistat24 =  $levfile[$aiStart+23]
    $aistat25 =  $levfile[$aiStart+24]
    $aistat26 =  $levfile[$aiStart+25]
    $aistat27 =  $levfile[$aiStart+26]
    $aistat28 =  $levfile[$aiStart+27]
    $aistat29 =  $levfile[$aiStart+28]
    $aistat30 =  $levfile[$aiStart+29]
    $aistat31 =  $levfile[$aiStart+30]
    $aistat32 =  $levfile[$aiStart+31]
    $aistat33 =  $levfile[$aiStart+32]
    $aistat34 =  $levfile[$aiStart+33]
    $aistat35 =  $levfile[$aiStart+34]
    $aistat36 =  $levfile[$aiStart+35]
    $aistat37 =  $levfile[$aiStart+36]
    $aistat38 =  $levfile[$aiStart+37]
    $aistat39 =  $levfile[$aiStart+38]
    $aistat40 =  $levfile[$aiStart+39]
    $aistat41 =  $levfile[$aiStart+40]
    $aistat42 =  $levfile[$aiStart+41]
    $aistat43 =  $levfile[$aiStart+42]
    $aistat44 =  $levfile[$aiStart+43]
    $aistat45 =  $levfile[$aiStart+44]
    $aistat46 =  $levfile[$aiStart+45]
    $aistat47 =  $levfile[$aiStart+46]
    $aistat48 =  $levfile[$aiStart+47]
    $aistat49 =  $levfile[$aiStart+48]
    $aistat50 =  $levfile[$aiStart+49]
    $aistat51 =  $levfile[$aiStart+50]
    $aistat52 =  $levfile[$aiStart+51]
    $aistat53 =  $levfile[$aiStart+52]
    $aistat54 =  $levfile[$aiStart+53]
    $aistat55 =  $levfile[$aiStart+54]
    $aistat56 =  $levfile[$aiStart+55]
    $aistat57 =  $levfile[$aiStart+56]
    $aistat58 =  $levfile[$aiStart+57]
    $aistat59 =  $levfile[$aiStart+58]
    $aistat60 =  $levfile[$aiStart+59]
    $aistat61 =  $levfile[$aiStart+60]
    $aistat62 =  $levfile[$aiStart+61]
    $aistat63 =  $levfile[$aiStart+62]
    $aistat64 =  $levfile[$aiStart+63]
    $aistat65 =  $levfile[$aiStart+64]
    $aistat66 =  $levfile[$aiStart+65]
    $aistat67 =  $levfile[$aiStart+66]
    $aistat68 =  $levfile[$aiStart+67]
    $aistat69 =  $levfile[$aiStart+68]
    $aistat70 =  $levfile[$aiStart+69]
    $aistat71 =  $levfile[$aiStart+70]
    $aistat72 =  $levfile[$aiStart+71]
    $aistat73 =  $levfile[$aiStart+72]
    $aistat74 =  $levfile[$aiStart+73]
    $aistat75 =  $levfile[$aiStart+74]
    $aistat76 =  $levfile[$aiStart+75]
    $aistat77 =  $levfile[$aiStart+76]
    $aistat78 =  $levfile[$aiStart+77]
    $aistat79 =  $levfile[$aiStart+78]
    $aistat80 =  $levfile[$aiStart+79]
    $aistat81 =  $levfile[$aiStart+80]
    $aistat82 =  $levfile[$aiStart+81]
    $aistat83 =  $levfile[$aiStart+82]
    $aistat84 =  $levfile[$aiStart+83]
    $aistat85 =  $levfile[$aiStart+84]
    $aistat86 =  $levfile[$aiStart+85]
    $aistat87 =  $levfile[$aiStart+86]
    $aistat88 =  $levfile[$aiStart+87]
    $aistat89 =  $levfile[$aiStart+88]
    $aistat90 =  $levfile[$aiStart+89]
    $aistat91 =  $levfile[$aiStart+90]
    $aistat92 =  $levfile[$aiStart+91]
    $aistat93 =  $levfile[$aiStart+92]
    $aistat94 =  $levfile[$aiStart+93]
    $aistat95 =  $levfile[$aiStart+94]
    $aistat96 =  $levfile[$aiStart+95]
    $aistat97 =  $levfile[$aiStart+96]
    $aistat98 =  $levfile[$aiStart+97]
    $aistat99 =  $levfile[$aiStart+98]
    $aistat100 =  $levfile[$aiStart+99]
    

    if ($Wnumber -gt  $numwizards){
    $wpresent = "No"
    }
    Else{
    $wpresent = "Yes"
    }

    $wizRow = $wizDatatable.NewRow()  
    $wizRow.WizardName = ($wname)
    $wizRow.Aggression = ($Aggression)
    $wizRow.Perception = ($Perception)
    $wizRow.Reflexes = ($Reflexes)
    $wizRow.CastleLevel = ($CastleLevel)
    $wizRow.PresentHidden = ($wpresent)
    $wizRow.SpellLoadout = ""   #Dummy this out for now as need the rest of the data in the table first to do spell lookup
    $wizRow.Fireball = ($Fireball)
    $wizRow.Shield = ($Shield)
    $wizRow.Accelerate = ($Accelerate)
    $wizRow.Possession = ($Possession)
    $wizRow.Health = ($Health)
    $wizRow.BeyondSight = ($BeyondSight)
    $wizRow.Earthquake = ($Earthquake)
    $wizRow.Meteor = ($Meteor)
    $wizRow.Volcano = ($Volcano)
    $wizRow.Crater = ($Crater)
    $wizRow.Teleport = ($Teleport)
    $wizRow.Duel = ($Duel)
    $wizRow.Invisible = ($Invisible)
    $wizRow.StealMana = ($StealMana)
    $wizRow.Rebound = ($Rebound)
    $wizRow.Lightning = ($Lightning)
    $wizRow.Castle = ($Castle)
    $wizRow.UndeadArmy = ($UndeadArmy)
    $wizRow.LightningStorm = ($LightningStorm)
    $wizRow.ManaMagnet = ($ManaMagnet)
    $wizRow.WallofFire = ($WallofFire)
    $wizRow.ReverseAcceleration = ($ReverseAcceleration)
    $wizRow.GlobalDeath = ($GlobalDeath)
    $wizRow.RapidFireball = ($RapidFireball)
    $wizRow.aistat = ($aistat)
    $wizRow.aistat2 = ($aistat2)
    $wizRow.aistat3 = ($aistat3)
    $wizRow.aistat4 = ($aistat4)
    $wizRow.aistat5 = ($aistat5)
    $wizRow.aistat6 = ($aistat6)
    $wizRow.aistat7 = ($aistat7)
    $wizRow.aistat8 = ($aistat8)
    $wizRow.aistat9 = ($aistat9)
    $wizRow.aistat10 = ($aistat10)
    $wizRow.aistat11 = ($aistat11)
    $wizRow.aistat12 = ($aistat12)
    $wizRow.aistat13 = ($aistat13)
    $wizRow.aistat14 = ($aistat14)
    $wizRow.aistat15 = ($aistat15)
    $wizRow.aistat16 = ($aistat16)
    $wizRow.aistat17 = ($aistat17)
    $wizRow.aistat18 = ($aistat18)
    $wizRow.aistat19 = ($aistat19)
    $wizRow.aistat20 = ($aistat20)
    $wizRow.aistat21 = ($aistat21)
    $wizRow.aistat22 = ($aistat22)
    $wizRow.aistat23 = ($aistat23)
    $wizRow.aistat24 = ($aistat24)
    $wizRow.aistat25 = ($aistat25)
    $wizRow.aistat26 = ($aistat26)
    $wizRow.aistat27 = ($aistat27)
    $wizRow.aistat28 = ($aistat28)
    $wizRow.aistat29 = ($aistat29)
    $wizRow.aistat30 = ($aistat30)
    $wizRow.aistat31 = ($aistat31)
    $wizRow.aistat32 = ($aistat32)
    $wizRow.aistat33 = ($aistat33)
    $wizRow.aistat34 = ($aistat34)
    $wizRow.aistat35 = ($aistat35)
    $wizRow.aistat36 = ($aistat36)
    $wizRow.aistat37 = ($aistat37)
    $wizRow.aistat38 = ($aistat38)
    $wizRow.aistat39 = ($aistat39)
    $wizRow.aistat40 = ($aistat40)
    $wizRow.aistat41 = ($aistat41)
    $wizRow.aistat42 = ($aistat42)
    $wizRow.aistat43 = ($aistat43)
    $wizRow.aistat44 = ($aistat44)
    $wizRow.aistat45 = ($aistat45)
    $wizRow.aistat46 = ($aistat46)
    $wizRow.aistat47 = ($aistat47)
    $wizRow.aistat48 = ($aistat48)
    $wizRow.aistat49 = ($aistat49)
    $wizRow.aistat50 = ($aistat50)
    $wizRow.aistat51 = ($aistat51)
    $wizRow.aistat52 = ($aistat52)
    $wizRow.aistat53 = ($aistat53)
    $wizRow.aistat54 = ($aistat54)
    $wizRow.aistat55 = ($aistat55)
    $wizRow.aistat56 = ($aistat56)
    $wizRow.aistat57 = ($aistat57)
    $wizRow.aistat58 = ($aistat58)
    $wizRow.aistat59 = ($aistat59)
    $wizRow.aistat60 = ($aistat60)
    $wizRow.aistat61 = ($aistat61)
    $wizRow.aistat62 = ($aistat62)
    $wizRow.aistat63 = ($aistat63)
    $wizRow.aistat64 = ($aistat64)
    $wizRow.aistat65 = ($aistat65)
    $wizRow.aistat66 = ($aistat66)
    $wizRow.aistat67 = ($aistat67)
    $wizRow.aistat68 = ($aistat68)
    $wizRow.aistat69 = ($aistat69)
    $wizRow.aistat70 = ($aistat70)
    $wizRow.aistat71 = ($aistat71)
    $wizRow.aistat72 = ($aistat72)
    $wizRow.aistat73 = ($aistat73)
    $wizRow.aistat74 = ($aistat74)
    $wizRow.aistat75 = ($aistat75)
    $wizRow.aistat76 = ($aistat76)
    $wizRow.aistat77 = ($aistat77)
    $wizRow.aistat78 = ($aistat78)
    $wizRow.aistat79 = ($aistat79)
    $wizRow.aistat80 = ($aistat80)
    $wizRow.aistat81 = ($aistat81)
    $wizRow.aistat82 = ($aistat82)
    $wizRow.aistat83 = ($aistat83)
    $wizRow.aistat84 = ($aistat84)
    $wizRow.aistat85 = ($aistat85)
    $wizRow.aistat86 = ($aistat86)
    $wizRow.aistat87 = ($aistat87)
    $wizRow.aistat88 = ($aistat88)
    $wizRow.aistat89 = ($aistat89)
    $wizRow.aistat90 = ($aistat90)
    $wizRow.aistat91 = ($aistat91)
    $wizRow.aistat92 = ($aistat92)
    $wizRow.aistat93 = ($aistat93)
    $wizRow.aistat94 = ($aistat94)
    $wizRow.aistat95 = ($aistat95)
    $wizRow.aistat96 = ($aistat96)
    $wizRow.aistat97 = ($aistat97)
    $wizRow.aistat98 = ($aistat98)
    $wizRow.aistat99 = ($aistat99)
    $wizRow.aistat100 = ($aistat100)


    $Wizdatatable.Rows.Add($wizRow)

    $wizardspells = SpellLoadout(($wnumber -1))
    $wizDetails.Rows[($wnumber -1)].Cells[6].Value = $wizardspells # Can't get spells until row is added to table, so go back and update it now it is

    $wnumber = $wnumber + 1
    $fpos = $fpos + 216
    $CLevel = $CLevel + 1

    }UNTIL ($wnumber -eq 9)

    if (-not(Test-Path -Path "$scriptdir/MCMaps/$fileonly.png" -PathType Leaf)) {
        $mapimgfile = (get-item "$scriptdir/MCMaps/BlankMap.png")
    }
    Else{$mapimgfile = (get-item "$scriptdir/MCMaps/$fileonly.png")}

  

    $Levelinfobox.text ="$fileonly.DAT
    Mana Target: $manatarget% of $manatotal total"

    $global:levimg = [System.Drawing.Image]::Fromfile($mapimgfile);
  
    $pictureBox.Image = $levimg
    $pictureBox.Size = New-Object System.Drawing.Size(256,256)
   

    #New attempt at drawing map
    $global:graphics=[System.Drawing.Graphics]::FromImage($levimg)
    $graphics.DrawImage($bmp,0,0,256,256)
    $picturebox.refresh()
}

function clearRow(){

    $currow = $datagridview.CurrentCell.RowIndex

    $datagridview.Rows[$currow].Cells[1].Value = 0
    $datagridview.Rows[$currow].Cells[2].Value = 0
    $datagridview.Rows[$currow].Cells[3].Value = 0
    $datagridview.Rows[$currow].Cells[4].Value = "Blank"
    $datagridview.Rows[$currow].Cells[5].Value = 0
    $datagridview.Rows[$currow].Cells[6].Value = 0
    $datagridview.Rows[$currow].Cells[7].Value = 0
    $datagridview.Rows[$currow].Cells[8].Value = 0
    $datagridview.Rows[$currow].Cells[9].Value = 0
    $datagridview.Rows[$currow].Cells[10].Value = 0
    $datagridview.Rows[$currow].Cells[11].Value = 0
    $datagridview.Rows[$currow].Cells[12].Value = "Blank"
    $datagridview.Rows[$currow].Cells[13].Value = "Blank"

}



#Datatable definitions

$levelDataTable = New-Object System.Data.DataTable

[void]$levelDataTable.Columns.Add('ManaTarget',[int]) 
[void]$levelDataTable.Columns.Add('NumWizards',[int]) 


$mapGenDataTable = New-Object System.Data.DataTable

[void]$mapGenDataTable.Columns.Add('ManaTotal',[int]) 
[void]$mapGenDataTable.Columns.Add('Seed',[int]) 
[void]$mapGenDataTable.Columns.Add('Offset',[int]) 
[void]$mapGenDataTable.Columns.Add('Raise',[int]) 
[void]$mapGenDataTable.Columns.Add('Gnarl',[int]) 
[void]$mapGenDataTable.Columns.Add('River',[int]) 
[void]$mapGenDataTable.Columns.Add('Sourc',[int]) 
[void]$mapGenDataTable.Columns.Add('SnLin',[int]) 
[void]$mapGenDataTable.Columns.Add('SnFlt',[int]) 
[void]$mapGenDataTable.Columns.Add('BhLin',[int]) 
[void]$mapGenDataTable.Columns.Add('BhFlt',[int]) 
[void]$mapGenDataTable.Columns.Add('RkSte',[int]) 

$Datatable = New-Object System.Data.DataTable

[void]$Datatable.Columns.Add("ThingNo",[int]) 
[void]$Datatable.Columns.Add("Class",[int])
[void]$Datatable.Columns.Add("ThingTypeHidden",[string])
[void]$Datatable.Columns.Add("Model",[int])
[void]$Datatable.Columns.Add("ThingNameHidden",[string]) 
[void]$Datatable.Columns.Add("XPos",[int])
[void]$Datatable.Columns.Add("YPos",[int])
[void]$Datatable.Columns.Add("DisId",[int])
[void]$Datatable.Columns.Add("SwiSz",[int])
[void]$Datatable.Columns.Add("SwiId",[int])
[void]$Datatable.Columns.Add("Parent",[int])
[void]$Datatable.Columns.Add("Child",[int])


$WizDatatable = New-Object System.Data.DataTable

[void]$WizDatatable.Columns.Add('WizardName',[string]) 
[void]$WizDatatable.Columns.Add('Aggression',[int]) 
[void]$WizDatatable.Columns.Add('Perception',[int]) 
[void]$WizDatatable.Columns.Add('Reflexes',[int]) 
[void]$WizDatatable.Columns.Add('CastleLevel',[int]) 
[void]$WizDatatable.Columns.Add('PresentHidden',[string]) 
[void]$WizDatatable.Columns.Add('SpellLoadout',[string]) 
[void]$WizDatatable.Columns.Add('Fireball',[boolean])
[void]$WizDatatable.Columns.Add('Shield',[boolean])
[void]$WizDatatable.Columns.Add('Accelerate',[boolean])
[void]$WizDatatable.Columns.Add('Possession',[boolean])
[void]$WizDatatable.Columns.Add('Health',[boolean])
[void]$WizDatatable.Columns.Add('BeyondSight',[boolean])
[void]$WizDatatable.Columns.Add('Earthquake',[boolean])
[void]$WizDatatable.Columns.Add('Meteor',[boolean])
[void]$WizDatatable.Columns.Add('Volcano',[boolean])
[void]$WizDatatable.Columns.Add('Crater',[boolean])
[void]$WizDatatable.Columns.Add('Teleport',[boolean])
[void]$WizDatatable.Columns.Add('Duel',[boolean])
[void]$WizDatatable.Columns.Add('Invisible',[boolean])
[void]$WizDatatable.Columns.Add('StealMana',[boolean])
[void]$WizDatatable.Columns.Add('Rebound',[boolean])
[void]$WizDatatable.Columns.Add('Lightning',[boolean])
[void]$WizDatatable.Columns.Add('Castle',[boolean])
[void]$WizDatatable.Columns.Add('UndeadArmy',[boolean])
[void]$WizDatatable.Columns.Add('LightningStorm',[boolean])
[void]$WizDatatable.Columns.Add('ManaMagnet',[boolean])
[void]$WizDatatable.Columns.Add('WallofFire',[boolean])
[void]$WizDatatable.Columns.Add('ReverseAcceleration',[boolean])
[void]$WizDatatable.Columns.Add('GlobalDeath',[boolean])
[void]$WizDatatable.Columns.Add('RapidFireball',[boolean])
[void]$WizDatatable.Columns.Add('aistat',[boolean])
[void]$WizDatatable.Columns.Add('aistat2',[boolean])
[void]$WizDatatable.Columns.Add('aistat3',[boolean])
[void]$WizDatatable.Columns.Add('aistat4',[boolean])
[void]$WizDatatable.Columns.Add('aistat5',[boolean])
[void]$WizDatatable.Columns.Add('aistat6',[boolean])
[void]$WizDatatable.Columns.Add('aistat7',[boolean])
[void]$WizDatatable.Columns.Add('aistat8',[boolean])
[void]$WizDatatable.Columns.Add('aistat9',[boolean])
[void]$WizDatatable.Columns.Add('aistat10',[boolean])
[void]$WizDatatable.Columns.Add('aistat11',[boolean])
[void]$WizDatatable.Columns.Add('aistat12',[boolean])
[void]$WizDatatable.Columns.Add('aistat13',[boolean])
[void]$WizDatatable.Columns.Add('aistat14',[boolean])
[void]$WizDatatable.Columns.Add('aistat15',[boolean])
[void]$WizDatatable.Columns.Add('aistat16',[boolean])
[void]$WizDatatable.Columns.Add('aistat17',[boolean])
[void]$WizDatatable.Columns.Add('aistat18',[boolean])
[void]$WizDatatable.Columns.Add('aistat19',[boolean])
[void]$WizDatatable.Columns.Add('aistat20',[boolean])
[void]$WizDatatable.Columns.Add('aistat21',[boolean])
[void]$WizDatatable.Columns.Add('aistat22',[boolean])
[void]$WizDatatable.Columns.Add('aistat23',[boolean])
[void]$WizDatatable.Columns.Add('aistat24',[boolean])
[void]$WizDatatable.Columns.Add('aistat25',[boolean])
[void]$WizDatatable.Columns.Add('aistat26',[boolean])
[void]$WizDatatable.Columns.Add('aistat27',[boolean])
[void]$WizDatatable.Columns.Add('aistat28',[boolean])
[void]$WizDatatable.Columns.Add('aistat29',[boolean])
[void]$WizDatatable.Columns.Add('aistat30',[boolean])
[void]$WizDatatable.Columns.Add('aistat31',[boolean])
[void]$WizDatatable.Columns.Add('aistat32',[boolean])
[void]$WizDatatable.Columns.Add('aistat33',[boolean])
[void]$WizDatatable.Columns.Add('aistat34',[boolean])
[void]$WizDatatable.Columns.Add('aistat35',[boolean])
[void]$WizDatatable.Columns.Add('aistat36',[boolean])
[void]$WizDatatable.Columns.Add('aistat37',[boolean])
[void]$WizDatatable.Columns.Add('aistat38',[boolean])
[void]$WizDatatable.Columns.Add('aistat39',[boolean])
[void]$WizDatatable.Columns.Add('aistat40',[boolean])
[void]$WizDatatable.Columns.Add('aistat41',[boolean])
[void]$WizDatatable.Columns.Add('aistat42',[boolean])
[void]$WizDatatable.Columns.Add('aistat43',[boolean])
[void]$WizDatatable.Columns.Add('aistat44',[boolean])
[void]$WizDatatable.Columns.Add('aistat45',[boolean])
[void]$WizDatatable.Columns.Add('aistat46',[boolean])
[void]$WizDatatable.Columns.Add('aistat47',[boolean])
[void]$WizDatatable.Columns.Add('aistat48',[boolean])
[void]$WizDatatable.Columns.Add('aistat49',[boolean])
[void]$WizDatatable.Columns.Add('aistat50',[boolean])
[void]$WizDatatable.Columns.Add('aistat51',[boolean])
[void]$WizDatatable.Columns.Add('aistat52',[boolean])
[void]$WizDatatable.Columns.Add('aistat53',[boolean])
[void]$WizDatatable.Columns.Add('aistat54',[boolean])
[void]$WizDatatable.Columns.Add('aistat55',[boolean])
[void]$WizDatatable.Columns.Add('aistat56',[boolean])
[void]$WizDatatable.Columns.Add('aistat57',[boolean])
[void]$WizDatatable.Columns.Add('aistat58',[boolean])
[void]$WizDatatable.Columns.Add('aistat59',[boolean])
[void]$WizDatatable.Columns.Add('aistat60',[boolean])
[void]$WizDatatable.Columns.Add('aistat61',[boolean])
[void]$WizDatatable.Columns.Add('aistat62',[boolean])
[void]$WizDatatable.Columns.Add('aistat63',[boolean])
[void]$WizDatatable.Columns.Add('aistat64',[boolean])
[void]$WizDatatable.Columns.Add('aistat65',[boolean])
[void]$WizDatatable.Columns.Add('aistat66',[boolean])
[void]$WizDatatable.Columns.Add('aistat67',[boolean])
[void]$WizDatatable.Columns.Add('aistat68',[boolean])
[void]$WizDatatable.Columns.Add('aistat69',[boolean])
[void]$WizDatatable.Columns.Add('aistat70',[boolean])
[void]$WizDatatable.Columns.Add('aistat71',[boolean])
[void]$WizDatatable.Columns.Add('aistat72',[boolean])
[void]$WizDatatable.Columns.Add('aistat73',[boolean])
[void]$WizDatatable.Columns.Add('aistat74',[boolean])
[void]$WizDatatable.Columns.Add('aistat75',[boolean])
[void]$WizDatatable.Columns.Add('aistat76',[boolean])
[void]$WizDatatable.Columns.Add('aistat77',[boolean])
[void]$WizDatatable.Columns.Add('aistat78',[boolean])
[void]$WizDatatable.Columns.Add('aistat79',[boolean])
[void]$WizDatatable.Columns.Add('aistat80',[boolean])
[void]$WizDatatable.Columns.Add('aistat81',[boolean])
[void]$WizDatatable.Columns.Add('aistat82',[boolean])
[void]$WizDatatable.Columns.Add('aistat83',[boolean])
[void]$WizDatatable.Columns.Add('aistat84',[boolean])
[void]$WizDatatable.Columns.Add('aistat85',[boolean])
[void]$WizDatatable.Columns.Add('aistat86',[boolean])
[void]$WizDatatable.Columns.Add('aistat87',[boolean])
[void]$WizDatatable.Columns.Add('aistat88',[boolean])
[void]$WizDatatable.Columns.Add('aistat89',[boolean])
[void]$WizDatatable.Columns.Add('aistat90',[boolean])
[void]$WizDatatable.Columns.Add('aistat91',[boolean])
[void]$WizDatatable.Columns.Add('aistat92',[boolean])
[void]$WizDatatable.Columns.Add('aistat93',[boolean])
[void]$WizDatatable.Columns.Add('aistat94',[boolean])
[void]$WizDatatable.Columns.Add('aistat95',[boolean])
[void]$WizDatatable.Columns.Add('aistat96',[boolean])
[void]$WizDatatable.Columns.Add('aistat97',[boolean])
[void]$WizDatatable.Columns.Add('aistat98',[boolean])
[void]$WizDatatable.Columns.Add('aistat99',[boolean])
[void]$WizDatatable.Columns.Add('aistat100',[boolean])



# ==================Forms start==================


[void][reflection.assembly]::LoadWithPartialName( "System.Windows.Forms")
$form = New-Object Windows.Forms.Form
$form.text = "MCLevelEdit  v0.2   By Moburma"
$Form.Location= New-Object System.Drawing.Size(100,100)
$Form.Size= New-Object System.Drawing.Size(960,795)

$Scriptpath = $PSCommandPath
$global:scriptdir = Split-Path $Scriptpath -Parent #Use script directory as default directory

$mapimgfile = (get-item "$scriptdir\MCMaps\Splash.png")
$img = [System.Drawing.Image]::Fromfile($mapimgfile);

[System.Windows.Forms.Application]::EnableVisualStyles();

$global:pictureBox = New-Object Windows.Forms.PictureBox
$pictureBox.Location = New-Object System.Drawing.Size(10,10)
$pictureBox.Size = New-Object System.Drawing.Size($img.Width,$img.Height)
$pictureBox.Image = $img
$Form.controls.add($pictureBox)


$thingNameCombo = @("Blank", "Unknown", "Wind", "Dragon", "Vulture", "Bee", "Worm", "Archer", "Crab", "Kraken", "Troll", "Griffin", "Skeleton", "Emu", "Genie", "Builder", "Townie", "Trader", "Wyvern", "Tree", "Standing Stone", "Dolmen", "Bad Stone", "2D Dome", "Big explosion", "Splash", "Fire", "Mini Volcano", "Volcano", "Mini crater", "Crater", "White smoke", "Black smoke", "Earthquake", "Meteor", "Steal Mana", "Lightning", "Rain of Fire", "Wall", "Path", "Canyon", "Teleport", "Mana Ball", "Villager Building", "Ridge Node", "Crab Egg", "Hidden Inside", "Hidden outside", "Hidden Inside re", "On victory", "Death Inside", "Death Outside", "Death inside re", "Obvious Inside", "Obvious outside", "Dragon", "Vulture", "Bee", "None", "Archer", "Crab", "Kraken", "Troll", "Griffon", "Genie", "Wyvern", "Creature all", "Flyer1", "Flyer2", "Flyer3", "Flyer4", "Flyer5", "Flyer6", "Flyer7", "Flyer8", "Fireball", "Heal", "Speed Up", "Possession", "Shield", "Beyond Sight", "Earthquake", "Meteor", "Volcano", "Crater", "Teleport", "Duel", "Invisible", "Steal Mana", "Rebound", "Lightning", "Castle", "Skeleton", "Thunderbolt", "Mana Magnet", "Fire Wall", "Reverse Speed", "Global Death", "Rapid Fireball")


$Levelinfobox = New-Object Windows.Forms.Label
$Levelinfobox.Location = New-Object Drawing.Point 10,270
$Levelinfobox.Size = New-Object Drawing.Point 200,50
$Levelinfobox.text ="$filename 
Mana Target: $manatarget% of $manatotal total"

$Form.controls.add($Levelinfobox)

#Load Button

$LoadButton_click = {LoadLevel}

$LoadButton = New-Object System.Windows.Forms.Button
$LoadButton.Location = New-Object System.Drawing.Size(10,350)
$LoadButton.Size = New-Object System.Drawing.Size(50,23)
$LoadButton.Text = "Load"
$Form.Controls.Add($LoadButton)
$LoadButton.Add_Click($LoadButton_Click)

#SaveButton

$SaveButton_click = {SaveFile}

$SaveButton = New-Object System.Windows.Forms.Button
$SaveButton.Location = New-Object System.Drawing.Size(70,350)
$SaveButton.Size = New-Object System.Drawing.Size(50,23)
$SaveButton.Text = "Save"
$Form.Controls.Add($SaveButton)
$SaveButton.Add_Click($SaveButton_Click)

#Clear Row Button

$ClearButton_click = {ClearRow}

$ClearButton = New-Object System.Windows.Forms.Button
$ClearButton.Location = New-Object System.Drawing.Size(10,380)
$ClearButton.Size = New-Object System.Drawing.Size(70,23)
$ClearButton.Text = "Clear Row"
$Form.Controls.Add($ClearButton)
$ClearButton.Add_Click($ClearButton_Click)

#Main Datagrid cell edited activities

$datagridview_CellEndEdit=[System.Windows.Forms.DataGridViewCellEventHandler]{
    #Event Argument: $_ = [System.Windows.Forms.DataGridViewCellEventArgs]
        if ($datagridview.Columns[$_.ColumnIndex].Name -eq 'Class') #If updating Class number, update ThingType to match
        {
            $newThing = IdentifyThing($datagridview.Rows[$_.RowIndex].Cells[$_.ColumnIndex].Value)
            
            $datagridview.Rows[$_.RowIndex].Cells[12].Value = $newThing

            $datagridview.Rows[$_.RowIndex].Cells[13].Value = "Blank"    #Also reset the ThingName and Model values so we don't get errors 

            $datagridview.Rows[$_.RowIndex].Cells[3].Value = 0

        }

        if ($datagridview.Columns[$_.ColumnIndex].Name -eq 'ThingType') #If updating ThingType, update Class number to match
        {
            $newClass = reverseIdentifyThing($datagridview.Rows[$_.RowIndex].Cells[$_.ColumnIndex].Value)
            
            $datagridview.Rows[$_.RowIndex].Cells[1].Value = $newClass

            $datagridview.Rows[$_.RowIndex].Cells[13].Value = "Blank"    #Also reset the ThingName and Model values so we don't get errors 

            $datagridview.Rows[$_.RowIndex].Cells[3].Value = 0
        }
        if ($datagridview.Columns[$_.ColumnIndex].Name -eq 'ThingName') #If updating ThingName, update number
     {
            if ($datagridview.Rows[$_.RowIndex].Cells[1].Value -eq 2 ){ #Update scenery number
                $newScenery = reverseIdentifyScenery($datagridview.Rows[$_.RowIndex].Cells[$_.ColumnIndex].Value)
            
                $datagridview.Rows[$_.RowIndex].Cells[3].Value = $newScenery
            }

            if ($datagridview.Rows[$_.RowIndex].Cells[1].Value -eq 3 ){ #Update scenery number
                $newSpawn = reverseIdentifySpawn($datagridview.Rows[$_.RowIndex].Cells[$_.ColumnIndex].Value)
            
                $datagridview.Rows[$_.RowIndex].Cells[3].Value = $newSpawn
            }

            if ($datagridview.Rows[$_.RowIndex].Cells[1].Value -eq 5 ){ #Update Creature number
                $newCreature = reverseIdentifycreature($datagridview.Rows[$_.RowIndex].Cells[$_.ColumnIndex].Value)
            
                $datagridview.Rows[$_.RowIndex].Cells[3].Value = $newcreature
            }

            if ($datagridview.Rows[$_.RowIndex].Cells[1].Value -eq 7 ){ #Update Weather number
                $newWeather = reverseIdentifyweather($datagridview.Rows[$_.RowIndex].Cells[$_.ColumnIndex].Value)
            
                $datagridview.Rows[$_.RowIndex].Cells[3].Value = $newWeather
            }

            if ($datagridview.Rows[$_.RowIndex].Cells[1].Value -eq 10 ){ #Update Effect number
                $newEffect = reverseIdentifyEffect($datagridview.Rows[$_.RowIndex].Cells[$_.ColumnIndex].Value)
            
                $datagridview.Rows[$_.RowIndex].Cells[3].Value = $newEffect
            }
        
            if ($datagridview.Rows[$_.RowIndex].Cells[1].Value -eq 11 ){ #Update switch number
                $newSwitch = reverseIdentifySwitch($datagridview.Rows[$_.RowIndex].Cells[$_.ColumnIndex].Value)
            
                $datagridview.Rows[$_.RowIndex].Cells[3].Value = $newSwitch
            }

            if ($datagridview.Rows[$_.RowIndex].Cells[1].Value -eq 12 ){ #Update Spell number
                $newSpell = reverseIdentifySpell($datagridview.Rows[$_.RowIndex].Cells[$_.ColumnIndex].Value)
            
                $datagridview.Rows[$_.RowIndex].Cells[3].Value = $newSpell
            }
    }

    if ($datagridview.Columns[$_.ColumnIndex].Name -eq 'Model') #If updating Model, update Thingname to match
    {
        
        if ($datagridview.Rows[$_.RowIndex].Cells[1].Value -eq 2 ){ #Update scenery number
            $newScenery = IdentifyScenery($datagridview.Rows[$_.RowIndex].Cells[$_.ColumnIndex].Value)
        
            $datagridview.Rows[$_.RowIndex].Cells[13].Value = $newScenery
        }

        if ($datagridview.Rows[$_.RowIndex].Cells[1].Value -eq 3 ){ #Update scenery number
            $newSpawn = IdentifySpawn($datagridview.Rows[$_.RowIndex].Cells[$_.ColumnIndex].Value)
        
            $datagridview.Rows[$_.RowIndex].Cells[13].Value = $newSpawn
        }

        if ($datagridview.Rows[$_.RowIndex].Cells[1].Value -eq 5 ){ #Update Creature number
            $newCreature = Identifycreature($datagridview.Rows[$_.RowIndex].Cells[$_.ColumnIndex].Value)
        
            $datagridview.Rows[$_.RowIndex].Cells[13].Value = $newcreature
        }

        if ($datagridview.Rows[$_.RowIndex].Cells[1].Value -eq 7 ){ #Update Weather number
            $newWeather = Identifyweather($datagridview.Rows[$_.RowIndex].Cells[$_.ColumnIndex].Value)
        
            $datagridview.Rows[$_.RowIndex].Cells[13].Value = $newWeather
        }

        if ($datagridview.Rows[$_.RowIndex].Cells[1].Value -eq 10 ){ #Update Effect number
            $newEffect = IdentifyEffect($datagridview.Rows[$_.RowIndex].Cells[$_.ColumnIndex].Value)
        
            $datagridview.Rows[$_.RowIndex].Cells[13].Value = $newEffect
        }
    
        if ($datagridview.Rows[$_.RowIndex].Cells[1].Value -eq 11 ){ #Update switch number
            $newSwitch = IdentifySwitch($datagridview.Rows[$_.RowIndex].Cells[$_.ColumnIndex].Value)
        
            $datagridview.Rows[$_.RowIndex].Cells[13].Value = $newSwitch
        }

        if ($datagridview.Rows[$_.RowIndex].Cells[1].Value -eq 12 ){ #Update Spell number
            $newSpell = IdentifySpell($datagridview.Rows[$_.RowIndex].Cells[$_.ColumnIndex].Value)
        
            $datagridview.Rows[$_.RowIndex].Cells[13].Value = $newSpell
        }

    }

    }

 $datagridview_CellClick=[System.Windows.Forms.DataGridViewCellEventHandler]{
    #Event Argument: $_ = [System.Windows.Forms.DataGridViewCellEventArgs]
    
      $transparent = [System.Drawing.Color]::FromArgb(0,255,0,255) 
    #$bmp.SetPixel(($lastx), ($lasty), ([System.Drawing.Color]::FromArgb(0, 255, 0, 255)))
    #$bmp.SetPixel(($lastx), ($lasty), ('Red'))

    if ($lastThing -gt 0){
    $bmp.SetPixel(($lastx), ($lasty), ((thingColour($lastThing))))}

    
            
   #$datagridview.Rows[$datagridview.CurrentRow.Index].DefaultCellStyle.BackColor = [System.Drawing.Color]::White;
    $global:lastx = $datagridview.Rows[$_.RowIndex].Cells[5].Value
    $global:lasty = $datagridview.Rows[$_.RowIndex].Cells[6].Value
    $global:lastThing = $datagridview.Rows[$_.RowIndex].Cells[1].Value

   $bmp.SetPixel(($datagridview.Rows[$_.RowIndex].Cells[5].Value), ($datagridview.Rows[$_.RowIndex].Cells[6].Value), 'Orange')
   
   
   $graphics.DrawImage($bmp, 0, 0, 256, 256)    #force redraw of map now user has clicked somewhere else
   $picturebox.refresh()
    }

$dataGridView_CellBeginEdit=[System.Windows.Forms.DataGridViewCellCancelEventHandler]{
        #Event Argument: $_ = [System.Windows.Forms.DataGridViewCellEventArgs]
    if ($datagridview.Columns[$_.ColumnIndex].Name -eq 'ThingName') #If updating ThingName combo, change Datasource to filtered source for that ThingType
     {
        if ($datagridview.Rows[$_.RowIndex].Cells[1].Value -eq 0 ){
                        
            ([System.Windows.Forms.DataGridViewComboBoxCell]$datagridview.Rows[$_.RowIndex].Cells[4]).DataSource = $unknownCombo 
        }   
        if ($datagridview.Rows[$_.RowIndex].Cells[1].Value -eq 2 ){
                        
            ([System.Windows.Forms.DataGridViewComboBoxCell]$datagridview.Rows[$_.RowIndex].Cells[4]).DataSource = $sceneryCombo 
        }      
        if ($datagridview.Rows[$_.RowIndex].Cells[1].Value -eq 3 ){
                        
            ([System.Windows.Forms.DataGridViewComboBoxCell]$datagridview.Rows[$_.RowIndex].Cells[4]).DataSource = $spawnCombo 
        }
        if ($datagridview.Rows[$_.RowIndex].Cells[1].Value -eq 5 ){
                        
            ([System.Windows.Forms.DataGridViewComboBoxCell]$datagridview.Rows[$_.RowIndex].Cells[4]).DataSource = $creatureCombo 
        }      
        if ($datagridview.Rows[$_.RowIndex].Cells[1].Value -eq 7 ){
                        
            ([System.Windows.Forms.DataGridViewComboBoxCell]$datagridview.Rows[$_.RowIndex].Cells[4]).DataSource = $weatherCombo 
        }        
        if ($datagridview.Rows[$_.RowIndex].Cells[1].Value -eq 10 ){
                        
            ([System.Windows.Forms.DataGridViewComboBoxCell]$datagridview.Rows[$_.RowIndex].Cells[4]).DataSource = $effectCombo 
        }
        if ($datagridview.Rows[$_.RowIndex].Cells[1].Value -eq 11 ){
                        
            ([System.Windows.Forms.DataGridViewComboBoxCell]$datagridview.Rows[$_.RowIndex].Cells[4]).DataSource = $switchCombo 
        }   
        if ($datagridview.Rows[$_.RowIndex].Cells[1].Value -eq 12 ){
                        
            ([System.Windows.Forms.DataGridViewComboBoxCell]$datagridview.Rows[$_.RowIndex].Cells[4]).DataSource = $spellCombo 
        }             
    }
                
      
    }

    

#Main Datagridview
$datagridview = New-Object System.Windows.Forms.DataGridView
$Form.controls.add($datagridview)
$datagridview.Location = New-Object System.Drawing.Point(280,10)
$datagridview.width = 650
$datagridview.height = 500
$datagridview.Add_CellBeginEdit($datagridview_CellBeginEdit)
$datagridview.Add_CellEndEdit($datagridview_CellEndEdit)
$datagridview.Add_CellClick($datagridview_CellClick)



$DataBindingSource = New-Object System.Windows.Forms.BindingSource;
$DataBindingSource.DataSource = $Datatable;

$datagridview.DataSource = $DataBindingSource
$datagridview.Columns[0].Width = 50 #Size up main columns
$datagridview.Columns[1].Width = 40
$datagridview.Columns[2].Width = 100
$datagridview.Columns[3].Width = 40
$datagridview.Columns[5].Width = 60
$datagridview.Columns[6].Width = 40
$datagridview.Columns[7].Width = 40
$datagridview.Columns[8].Width = 40
$datagridview.Columns[9].Width = 40
$datagridview.Columns[10].Width = 40
$datagridview.Columns[11].Width = 40

$datagridview.Columns[2].Visible = $false;  # Hide ThingType column to substitute with combobox
$datagridview.Columns[4].Visible = $false;  # Hide ThingName column to substitute with combobox

$datagridview.Columns[0].ReadOnly = $true; # Don't let user change Thing Number value

$DataGridView.AllowUserToAddRows = $false; # Don't let user add new rows

$ThingTypeColumn = New-Object System.Windows.Forms.DataGridViewComboBoxColumn   #Define ThingType Combobox
$ThingTypeColumn.width = 80
$ThingTypeColumn.HeaderText = "ThingType"
$ThingTypeColumn.name = "ThingType"
$ThingTypeColumn.DataPropertyName = 'ThingTypeHidden'
$ThingTypeColumn.DataSource = $classcombo 
[void]$datagridview.Columns.Add($ThingTypeColumn)
$ThingTypeColumn.DisplayIndex = 3

$ThingNameColumn = New-Object System.Windows.Forms.DataGridViewComboBoxColumn   #Define ThingName Combobox
$ThingNameColumn.width = 80
$ThingNameColumn.HeaderText = "ThingName"
$ThingNameColumn.name = "ThingName"
$ThingNameColumn.DataPropertyName = 'ThingNameHidden'
$ThingNameColumn.DataSource = $thingNameCombo
[void]$datagridview.Columns.Add($ThingNameColumn)
$ThingNameColumn.DisplayIndex = 5


# Wizard View

$WizDetails_CellEndEdit=[System.Windows.Forms.DataGridViewCellEventHandler]{
    #Event Argument: $_ = [System.Windows.Forms.DataGridViewCellEventArgs]
    if ($WizDetails.Columns[$_.ColumnIndex].Index -gt 6){
        $wizDetails.Rows[$_.RowIndex].Cells[6].Value = SpellLoadout($_.RowIndex)

    }

    if ($WizDetails.Rows[$_.RowIndex].Cells[5].Value -eq "Yes"){    #If a wizard is set to being present, set all previous wizards to Yes as well
        $wUpdates = $_.rowindex 
        $levelDataTable.rows[0][1] = ($_.rowindex + 1) #Update total number of wizards
        
        DO{
            $WizDetails.Rows[$wUpdates].Cells[5].Value = "Yes"
            $wUpdates--
        }UNTIL ($wUpdates -eq 0)
    

    }

    if ($WizDetails.Rows[$_.RowIndex].Cells[5].Value -eq "No"){    #If a wizard is set to not being present, set all subsequent wizards to No as well
        $wUpdates = ($_.rowindex + 1)
        $levelDataTable.rows[0][1] = $wUpdates   #Update total number of wizards
        
        DO{
            $WizDetails.Rows[$wUpdates].Cells[5].Value = "No"
            $wUpdates++
        }UNTIL ($wUpdates -eq 8)
    

    }
   

}


$WizDetails = New-Object System.Windows.Forms.DataGridView
$Form.controls.add($WizDetails)
$WizDetails.Location = New-Object System.Drawing.Point(280,520)
$WizDetails.width = 650
$WizDetails.height = 220
$WizDetails.Add_CellEndEdit($WizDetails_CellEndEdit)

$wizDataBindingSource = New-Object System.Windows.Forms.BindingSource;
$wizDataBindingSource.DataSource = $wizDatatable;

$WizDetails.DataSource = $wizDataBindingSource
$WizDetails.Columns[0].Width = 80
$WizDetails.Columns[1].Width = 60
$WizDetails.Columns[2].Width = 70
$WizDetails.Columns[3].Width = 60
$WizDetails.Columns[5].Width = 60
$WizDetails.Columns[6].Width = 280

$aicount = 31
DO{     # Hide all the ai Boolean values

    $WizDetails.Columns[$aicount].Visible = $false; 
    $aicount++

}UNTIL ($aicount -eq 131)

$PresentColumn = New-Object System.Windows.Forms.DataGridViewComboBoxColumn   #Define ThingType Combobox
$PresentColumn.width = 80
$PresentColumn.HeaderText = "Present"
$PresentColumn.name = "Present"
$PresentColumn.DataPropertyName = 'PresentHidden'
$PresentColumn.DataSource = $presentCombo 
[void]$WizDetails.Columns.Add($PresentColumn)
$PresentColumn.DisplayIndex = 4

$WizDetails.Columns[5].Visible = $false;

$WizDetails.Columns[0].Readonly = $true; # Don't let user edit Wizard names
#$WizDetails.Columns[5].Readonly = $true; # Don't let user edit if Wizard is present on map (this should be automatic)
$WizDetails.Columns[6].Readonly = $true; # Don't let user edit spell loadout summary; this is updated based on Booleans

$WizDetails.AllowUserToAddRows = $false; # Don't let user add more rows




$form.ShowDialog()