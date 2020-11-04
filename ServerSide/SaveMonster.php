<?php
	$db = mysqli_connect("localhost:3306", "joshuagr", "legoguitarclayrobot") or die("Could not connect: " . mysqli_error($db));
    mysqli_select_db($db, "joshuagr_3DMonsters") or die("Could not select database: joshuagr_3DMonsters");

    $secretKey = "gamesvideoyeahfuck";

    // Strings are escaped to prevent SQL injection attack.
    $User = $_POST['User'];
    $Profile = $_POST['Profile'];
    $Monster = $_POST['Monster'];
    $hash = $_POST['hash'];

    $real_hash = md5($User . $Profile . $Monster . $secretKey);
    if($real_hash == $hash) {
    	$User = mysqli_real_escape_string($db, $User);
    	$Profile = mysqli_real_escape_string($db, $Profile);
    	$Monster = mysqli_real_escape_string($db, $Monster);
    	$query = "INSERT INTO MonsterCreations (User, Profile, Monster) VALUES (\"$User\", \"$Profile\", \"$Monster\");";
        $result = mysqli_query($db, $query) or die("Query failed: " . mysqli_error($db));
        echo "Monster Saved!";
    }