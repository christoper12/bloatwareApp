Imports System.ComponentModel

Public Class SortableBindingList(Of T)
    Inherits BindingList(Of T)

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(list As IList(Of T))
        MyBase.New(list)
    End Sub

    Private _isSorted As Boolean
    Private _sortDirection As ListSortDirection
    Private _sortProperty As PropertyDescriptor

    Protected Overrides ReadOnly Property SupportsSortingCore As Boolean
        Get
            Return True
        End Get
    End Property

    Protected Overrides ReadOnly Property IsSortedCore As Boolean
        Get
            Return _isSorted
        End Get
    End Property

    Protected Overrides ReadOnly Property SortDirectionCore As ListSortDirection
        Get
            Return _sortDirection
        End Get
    End Property

    Protected Overrides ReadOnly Property SortPropertyCore As PropertyDescriptor
        Get
            Return _sortProperty
        End Get
    End Property

    Protected Overrides Sub ApplySortCore(
    prop As PropertyDescriptor,
    direction As ListSortDirection
)
        Dim list = CType(Items, List(Of T))

        list.Sort(Function(a, b)
                      Dim valA = prop.GetValue(a)
                      Dim valB = prop.GetValue(b)
                      Return If(direction = ListSortDirection.Ascending,
                            Comparer.Default.Compare(valA, valB),
                            Comparer.Default.Compare(valB, valA))
                  End Function)

        _sortProperty = prop
        _sortDirection = direction
        _isSorted = True

        OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
    End Sub
End Class