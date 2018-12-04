# Generated by Django 2.1a1 on 2018-11-21 05:30

from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    initial = True

    dependencies = [
    ]

    operations = [
        migrations.CreateModel(
            name='Actor',
            fields=[
                ('actor_id', models.IntegerField(primary_key=True, serialize=False)),
                ('actor_FIO', models.CharField(help_text='ФИО актера', max_length=100)),
                ('actor_data_birthday', models.DateField()),
                ('actors_contry', models.CharField(max_length=50)),
            ],
        ),
        migrations.CreateModel(
            name='Film',
            fields=[
                ('film_id', models.IntegerField(primary_key=True, serialize=False)),
                ('name_film', models.CharField(help_text='Название фильма', max_length=100)),
                ('year_of_release', models.IntegerField()),
                ('film_contry', models.CharField(max_length=50)),
                ('long', models.TimeField()),
                ('rating', models.IntegerField(max_length=5)),
                ('poster', models.ImageField(upload_to='')),
                ('trailer', models.FileField(upload_to='')),
                ('video', models.FileField(upload_to='')),
                ('actor_id', models.ManyToManyField(to='cinerama_app.Actor')),
            ],
        ),
        migrations.CreateModel(
            name='Film_company',
            fields=[
                ('company_id', models.IntegerField(primary_key=True, serialize=False)),
                ('name_company', models.CharField(help_text='Название компании', max_length=100)),
                ('year_of_foundation', models.IntegerField()),
            ],
        ),
        migrations.CreateModel(
            name='Genre',
            fields=[
                ('genre_id', models.IntegerField(primary_key=True, serialize=False)),
                ('ganre_name', models.CharField(max_length=30)),
            ],
        ),
        migrations.CreateModel(
            name='Producer',
            fields=[
                ('producer_id', models.IntegerField(primary_key=True, serialize=False)),
                ('producer_FIO', models.CharField(help_text='ФИО режиссера', max_length=100)),
                ('producer_data_birthday', models.DateField()),
            ],
        ),
        migrations.AddField(
            model_name='film',
            name='company_id',
            field=models.ForeignKey(null=True, on_delete=django.db.models.deletion.SET_NULL, to='cinerama_app.Film_company'),
        ),
        migrations.AddField(
            model_name='film',
            name='genre_id',
            field=models.ManyToManyField(to='cinerama_app.Genre'),
        ),
        migrations.AddField(
            model_name='film',
            name='producer_id',
            field=models.ForeignKey(null=True, on_delete=django.db.models.deletion.SET_NULL, to='cinerama_app.Producer'),
        ),
    ]
