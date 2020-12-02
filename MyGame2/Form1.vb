﻿Public Class Form1
    Dim xdir As Integer
    Dim ydir As Integer

    Function Collision(p As String, t As String, Optional ByRef other As Object = vbNull)
        For Each c In Controls
            If c.name.toupper.ToString.Contains(p.ToUpper) Then
                Return Collision(c, t, other)
            End If
        Next
        Return False
    End Function
    Function Collision(p As PictureBox, t As String, Optional ByRef other As Object = vbNull)
        Dim col As Boolean

        For Each c In Controls
            Dim obj As Control
            obj = c
            If obj.Visible AndAlso p.Bounds.IntersectsWith(obj.Bounds) And obj.Name.ToUpper.Contains(t.ToUpper) Then
                col = True
                other = obj
            End If
        Next
        Return col
    End Function
    'Return true or false if moving to the new location is clear of objects ending with t
    Function IsClear(p As PictureBox, distx As Integer, disty As Integer, t As String) As Boolean
        Dim b As Boolean

        p.Location += New Point(distx, disty)
        b = Not Collision(p, t)
        p.Location -= New Point(distx, disty)
        Return b
    End Function
    'Moves and object (won't move onto objects containing  "wall" and shows green if object ends with "win"
    Sub MoveTo(p As PictureBox, distx As Integer, disty As Integer)
        If IsClear(p, distx, disty, "WALL") Then
            p.Location += New Point(distx, disty)
        End If
        Dim other As Object = Nothing
        If Collision("Bullet", "Astroid", other) Then
            other.visible = False
            Return

        End If
    End Sub
    Sub MoveTo(p As String, distx As Integer, disty As Integer)
        For Each c In Controls
            If c.name.toupper = p.ToUpper Then
                MoveTo(c, distx, disty)
            End If
        Next
    End Sub
    Sub CreateNew(name As String, pic As PictureBox, location As Point)
        Dim p As New PictureBox
        p.Location = location
        p.Image = pic.Image
        p.Name = name
        p.Width = pic.Width
        p.Height = pic.Height
        p.SizeMode = PictureBoxSizeMode.StretchImage
        Controls.Add(p)

    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        MoveTo("bulletright", 15, 0)
        MoveTo("bulletleft", -15, 0)
        MoveTo("bulletup", 0, -15)
        MoveTo("bulletdown", 0, 15)

    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Up
                xdir = 0
                ydir = -5
                MoveTo("Picturebox1", 0, -5)
            Case Keys.Down
                xdir = 0
                ydir = 5
                MoveTo("Picturebox1", 0, 5)
            Case Keys.Left
                xdir = -5
                ydir = 0
                MoveTo("Picturebox1", -5, 0)
            Case Keys.Right
                xdir = 5
                ydir = 0
                MoveTo("BulletRight", 5, 0)

            Case Keys.Space
                If xdir > 0 Then
                    CreateNew("Bulletright", PictureBox3, PictureBox1.Location)
                End If
                If xdir < 0 Then
                    CreateNew("Bulletleft", PictureBox3, PictureBox1.Location)
                End If
            Case Else

        End Select
    End Sub
End Class
