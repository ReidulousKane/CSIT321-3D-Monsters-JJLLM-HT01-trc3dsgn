<?php
	$db = mysqli_connect("localhost", "root", "root") or die("Could not connect: " . mysqli_error($db));
    mysqli_select_db($db, "test") or die("Could not select database: test");

    $secretKey="gamesvideoyeahfuck";

    $testUserName =  $_POST['testUserName'];
    $testOldPWHash = $_POST['testOldPWHash'];
    $testNewPWHash = $_POST['testNewPWHash'];
    $hash = $_POST['hash'];

    $real_hash = md5($testUserName . $testOldPWHash . $testNewPWHash . $secretKey);
    if($real_hash == $hash)
    {
    	$testUserName = mysqli_real_escape_string($db, $testUserName);
	    $query = "SELECT testPHash FROM testUserRegistrar WHERE testUsername = \"$testUserName\"";
	    $sqlResult = mysqli_query($db, $query) or die("Query failed: " . mysqli_error($db));
	    $result = mysqli_fetch_row($sqlResult);
	    if ($testOldPWHash == $result[0])
	    {
	    	$changePWQuery = "UPDATE testUserRegistrar SET testPHash = \"$testNewPWHash\" WHERE testUsername = \"$testUserName\"";
	    	$changeSqlResult = mysqli_query($db, $changePWQuery) or die("Query failed: " . mysqli_error($db));
	    	echo "Successfully changed Password!";
	    }
	    else
	    {
	    	echo "Incorrect current password!";
	    }
    }