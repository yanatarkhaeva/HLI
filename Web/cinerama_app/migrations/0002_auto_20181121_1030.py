# Generated by Django 2.1a1 on 2018-11-21 05:30

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('cinerama_app', '0001_initial'),
    ]

    operations = [
        migrations.AlterField(
            model_name='film',
            name='rating',
            field=models.IntegerField(),
        ),
    ]