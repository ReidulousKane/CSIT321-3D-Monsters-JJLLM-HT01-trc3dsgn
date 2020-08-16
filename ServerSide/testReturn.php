<?php
    // Send variables for the MySQL database class.
	$db = mysqli_connect("localhost", "root", "root") or die("Could not connect: " . mysqli_error()); 
    mysqli_select_db($db, "test") or die("Could not select database");
 
    $query = "SELECT `testString` FROM `testConnection` ORDER by `id` DESC LIMIT 1";
    $result = mysqli_query($db, $query) or die("Query failed: " . mysql_error());

    // this is not yet working, yay for learning new stuff!
    $row = mysqli_use_result($db);
 
    echo "Server sent: " . $row["testString"];
