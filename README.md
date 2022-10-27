
# Social Brothers case

In this assignment i have made a small application where you (add/read/edit/edit) addresses with the possibility to use filters and to sort data dynamically.
You have also the possibility to calculate distances between two addresses.


## Run Locally

Clone the project

```bash
  git clone https://github.com/karim-chakroun/Social-Brothers-case
```




## Demo

Here is the link: https://localhost:7114/swagger/index.html


## Steps:

- First you need to run the project
![App Screenshot](https://media.discordapp.net/attachments/711915490091597874/1035324493738037268/unknown.png?width=1105&height=70)
- After running this page will show up
![App Screenshot](https://media.discordapp.net/attachments/711915490091597874/1035323434189733928/unknown.png?width=1105&height=466)
- You can add a new address
![App Screenshot](https://media.discordapp.net/attachments/711915490091597874/1035327863320223895/unknown.png?width=1105&height=469)
- You can get a single address by putting the Id
![App Screenshot](https://media.discordapp.net/attachments/711915490091597874/1035328279122554900/unknown.png?width=1105&height=319)
- You can edit a single address by putting the Id
![App Screenshot](https://media.discordapp.net/attachments/711915490091597874/1035328737547407370/unknown.png?width=1105&height=384)
- You can delete a single address by putting the Id
![App Screenshot](https://media.discordapp.net/attachments/711915490091597874/1035329075306319922/unknown.png?width=1105&height=293)

#### You can filter the addresses by :
- Typing the keyword in the filter field.
- Typing the attribute you wanna order by (country,street,id,houseNumber,zipCode,city or you can add a new attribute and name it) in the attribute field.
- Chosing the desc field as true or false( true means descending and false means ascending sort).


![App Screenshot](https://media.discordapp.net/attachments/711915490091597874/1035329766292717598/unknown.png?width=1105&height=373)
- You calculate the distance between two adresses by putting the Id of the addresses
![App Screenshot](https://media.discordapp.net/attachments/711915490091597874/1035333996009230406/unknown.png?width=1105&height=288)

## Feedback

😄 I'm proud of the dynamic filter and sorting part. It was a great challenge for me, first I made a static filter. You can check it in the first commit but then I made it.

🤔 The code can be better. There is a bug if you try putting a filter without a sorting attribute you will have error 500. It needs to be fixed but I didn't have time to finish it but I think that I made it through the hard part.
